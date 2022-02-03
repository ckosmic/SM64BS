using LibSM64;
using SM64BS.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SM64BS.Behaviours
{
	internal class MarioSpecialEffects : MonoBehaviour
    {
        private Material _material;
        private AnimationCurve _easeOut;
        private AnimationCurve _easeIn;
        private ResourceUtilities _utils;

        private GameObject _outParticlesPrefab;
        private GameObject _inParticlesPrefab;
        private GameObject _ringPrefab;
        private GameObject _popParticlesPrefab;

        public void Initialize(ResourceUtilities utils)
        {
            _utils = utils;

            _easeOut = AnimationCurve.EaseInOut(0, 0, 1, 1);
            Keyframe[] keys = _easeOut.keys;
            keys[0].outTangent = Mathf.Exp(1);
            _easeOut.keys = keys;
            _easeIn = AnimationCurve.EaseInOut(0, 0, 1, 1);
            keys = _easeIn.keys;
            keys[1].inTangent = Mathf.Exp(1);
            _easeIn.keys = keys;

            _outParticlesPrefab = _utils.LoadAssetFromMainBundle<GameObject>("Assets/SM64BS/01.prefab");
            _inParticlesPrefab = _utils.LoadAssetFromMainBundle<GameObject>("Assets/SM64BS/01In.prefab");
            _popParticlesPrefab = _utils.LoadAssetFromMainBundle<GameObject>("Assets/SM64BS/Pop.prefab");
            _ringPrefab = _utils.LoadAssetFromMainBundle<GameObject>("Assets/SM64BS/Ring.prefab");
        }

        public void SpawnPopParticles()
        {
            GameObject particles = Instantiate(_popParticlesPrefab, transform.position + Vector3.up * 0.3f, Quaternion.Euler(-90f, 0f, 0f));
            particles.transform.localScale = Vector3.one * 0.5f;
            particles.transform.LookAt(Camera.main.transform);
            Destroy(particles, 1.0f);
        }

        public void TeleportOut(Action callback, float callbackDelay)
        {
            GameObject particles = Instantiate(_outParticlesPrefab, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            particles.transform.localScale = Vector3.one * 0.25f;
            Destroy(particles, 1.5f);
            StartCoroutine(Teleport(0, callback, callbackDelay));
        }

        public void TeleportIn(Action callback, float callbackDelay)
        {
            GameObject particles = Instantiate(_inParticlesPrefab, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            particles.transform.localScale = Vector3.one * 0.25f;
            Destroy(particles, 2.0f);
            StartCoroutine(Teleport(1, callback, callbackDelay));
        }

        IEnumerator Teleport(int direction, Action callback, float delay)
        {
            if(_material == null)
                _material = GetComponent<SM64Mario>().marioRendererObject.GetComponent<MeshRenderer>().sharedMaterial;

            yield return new WaitForSecondsRealtime(0.05f); // Account for Mario's vertex lerping

            Transform[] ringTransforms = new Transform[8];
            for (int i = 0; i < ringTransforms.Length; i++)
            {
                ringTransforms[i] = Instantiate(_ringPrefab, transform.position, Quaternion.Euler(-90, 0, 0)).transform;
            }

            float t = direction;
            float scaleFactor = SM64Context.GetScaleFactor() * 100.0f;
            bool condition = direction.Equals(0) ? (t < 0.999f) : (t > 0.001f);
            while (condition)
            {
                Vector3 worldPos = transform.position;
                worldPos.z *= -1f;

                float xzStretch = 1f - _easeOut.Evaluate(t);
                float yStretch = 1f + _easeIn.Evaluate(t) * 3f;
                Vector3 worldPosScaled = worldPos * scaleFactor;
                Vector3 stretchVector = new Vector3(xzStretch, yStretch, xzStretch);
                _material.SetVector("_Stretch", stretchVector);
                _material.SetVector("_VertexOffset", Vector3.Scale(worldPosScaled, stretchVector) - worldPosScaled);
                _material.SetColor("_AdditiveColor", new Color(t, t, t));

                float dirT = direction == 0 ? 1f - t : t;
                for (int i = 0; i < ringTransforms.Length; i++)
                {
                    float byI = 1f / (i + 0.0001f);
                    ringTransforms[i].position = new Vector3(worldPos.x, worldPos.y + (1f - byI) * yStretch * t * 1.25f, -worldPos.z);
                    ringTransforms[i].localScale = Vector3.one * (byI * (1f-t) + 0.1f);
                    ringTransforms[i].GetComponent<MeshRenderer>().material.SetColor("_TintColor", new Color(1.0f, 1.0f, 1.0f, dirT * 0.25f));
                }

                t += Time.deltaTime * (direction == 0 ? 2f : -2f);
                condition = direction.Equals(0) ? (t < 0.999f) : (t > 0.001f);
                yield return new WaitForEndOfFrame();
            }

            if (direction == 0)
            {
                _material.SetVector("_Stretch", new Vector4(0f, 4f, 0f, 1f));
                _material.SetColor("_AdditiveColor", Color.white);
                _material.SetVector("_VertexOffset", Vector4.zero);
            }
            else
            {
                _material.SetVector("_Stretch", Vector4.one);
                _material.SetColor("_AdditiveColor", Color.black);
                _material.SetVector("_VertexOffset", new Vector4(0f, 0f, 0f, 1f));
            }
            for (int i = 0; i < ringTransforms.Length; i++)
            {
                Destroy(ringTransforms[i].gameObject);
                ringTransforms[i] = null;
            }

            yield return new WaitForSecondsRealtime(delay);

            if (callback != null)
                callback.Invoke();
        }
    }
}
