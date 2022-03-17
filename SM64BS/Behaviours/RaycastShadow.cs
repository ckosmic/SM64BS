using SM64BS.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SM64BS.Behaviours
{
    // I would use projectors but they're a mess
    public class RaycastShadow : MonoBehaviour
    {
        public ResourceUtilities utils;

        private GameObject _quad;
        private Transform _quadTransform;
        private Material _quadMaterial;

        private float _prevDistance = 0f;

        private void Start()
        {
            _quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
            _quadMaterial = utils.LoadAssetFromMainBundle<Material>("ShadowMaterial.mat");
            _quad.GetComponent<MeshRenderer>().material = _quadMaterial;
            _quadTransform = _quad.transform;
            _quadTransform.SetParent(null);
            _quadTransform.localScale = Vector3.one * 0.6f;
            Destroy(_quad.GetComponent<MeshCollider>());
        }

        private void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up * 0.01f, -Vector3.up, out hit))
            {
                _quad.transform.position = hit.point + hit.normal * 0.001f;
                _quad.transform.rotation = Quaternion.LookRotation(-hit.normal);
                if (_prevDistance != hit.distance)
                {
                    _quadTransform.localScale = Vector3.one * 0.6f / (hit.distance + 1);
                    _quadMaterial.SetColor("_Color", new Color(1f, 1f, 1f, Mathf.Clamp01(0.5f / (hit.distance + 1))));

                    _prevDistance = hit.distance;
                }
            }
        }
    }
}
