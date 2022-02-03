using LibSM64;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR;
using static SM64BS.Utils.Types;

namespace SM64BS.Behaviours
{
    internal class MarioBehaviour : MonoBehaviour
    {
        private bool _waved = false;
        private Camera _mainCamera;
        private SM64Mario _sm64Mario;
        private InputProvider _inputProvider;
        private GrabState _grabState;

        private Vector3[] velocityFrames = new Vector3[5];
        private int currentVelocityFrame = 0;
        private Vector3 throwVelocity;

        private void Start() 
        {
            _mainCamera = Camera.main;
            _sm64Mario = GetComponent<SM64Mario>();
            _inputProvider = GetComponent<InputProvider>();

            _sm64Mario.MarioStoppedMoving += MarioStoppedMoving;

            StartCoroutine(CheckForLookDirection());
        }

        private void Update()
        {
            if(_inputProvider.eitherControllerPresent)
                ManageGrabs();
        }

        private void FixedUpdate()
        {
            // https://karllewisdesign.com/how-to-improve-throwing-physics-in-vr/ <-- knawlege
            if (_inputProvider.eitherControllerPresent)
                VelocityUpdate();
        }

        private void OnDestroy()
        {
            _sm64Mario.MarioStoppedMoving -= MarioStoppedMoving;
        }

        private void ManageGrabs()
        {
            bool gripHeldL = _inputProvider.GetVRButtonHeld(CommonUsages.gripButton, VRControllerType.Left);
            bool gripHeldR = _inputProvider.GetVRButtonHeld(CommonUsages.gripButton, VRControllerType.Right);
            if (_grabState.isGrabbing)
            {
                Vector3 controllerPos = _inputProvider.GetVRHandPosition(_grabState.controllerType);
                Quaternion controllerRot = _inputProvider.GetVRHandRotation(_grabState.controllerType);
                _sm64Mario.SetAction(SM64MarioAction.ACT_GRABBED);
                _sm64Mario.SetVelocity(Vector3.zero);
                _sm64Mario.SetPosition(controllerPos);
                _sm64Mario.SetRotation(controllerRot);

                if ((_grabState.controllerType == VRControllerType.Left && !gripHeldL) || 
                    (_grabState.controllerType == VRControllerType.Right && !gripHeldR))
                {
                    Vector3 controllerVel = _inputProvider.GetVRHandVelocity(_grabState.controllerType);
                    Vector3 controllerAngVel = _inputProvider.GetVRHandAngularVelocity(_grabState.controllerType);

                    Vector3 controllerVelCross = Vector3.Cross(controllerAngVel, controllerPos);

                    ReleaseGrab(controllerVel, controllerAngVel, controllerVelCross);
                }
            }
            else
            {
                Vector3 controllerPosL = _inputProvider.GetVRHandPosition(VRControllerType.Left);
                float distanceL = Vector3.Distance(transform.position, controllerPosL);
                Vector3 controllerPosR = _inputProvider.GetVRHandPosition(VRControllerType.Right);
                float distanceR = Vector3.Distance(transform.position, controllerPosR);
                if (gripHeldL && distanceL <= 0.75f && distanceL < distanceR)
                {
                    _grabState.controllerType = VRControllerType.Left;
                    _grabState.isGrabbing = true;
                }

                if (_grabState.isGrabbing == false && gripHeldR && distanceR <= 0.75f && distanceR < distanceL)
                {
                    _grabState.controllerType = VRControllerType.Right;
                    _grabState.isGrabbing = true;
                }
            }
        }

        private void ReleaseGrab(Vector3 controllerVel, Vector3 controllerAngVel, Vector3 crossVel)
        {
            _grabState.isGrabbing = false;
            _grabState.controllerType = VRControllerType.None;

            throwVelocity = controllerVel + crossVel;

            AddVelocityHistory();

            Vector3 throwVelocityFlat = new Vector3(throwVelocity.x, 0f, throwVelocity.z);

            _sm64Mario.SetRotation(Quaternion.LookRotation(throwVelocityFlat));
            _sm64Mario.SetAction(SM64MarioAction.ACT_THROWN_FORWARD);
            _sm64Mario.SetVelocity(throwVelocity / 10f);
            _sm64Mario.SetFowardVelocity(throwVelocityFlat.magnitude / 10f);

            ResetVelocityHistory();
        }

        private void VelocityUpdate()
        {
            if (velocityFrames != null)
            {
                currentVelocityFrame++;
                if (currentVelocityFrame >= velocityFrames.Length)
                {
                    currentVelocityFrame = 0;
                }

                Vector3 controllerVel = _inputProvider.GetVRHandVelocity(_grabState.controllerType);

                velocityFrames[currentVelocityFrame] = controllerVel;
            }
        }

        private void AddVelocityHistory()
        {
            if (velocityFrames != null)
            {
                Vector3 velocityAverage = GetVectorAverage(velocityFrames);
                if (velocityAverage != null)
                {
                    throwVelocity = velocityAverage;
                }
            }
        }

        private void ResetVelocityHistory()
        {
            currentVelocityFrame = 0;
            if (velocityFrames != null && velocityFrames.Length > 0)
            { 
                velocityFrames = new Vector3[velocityFrames.Length];
            }
        }

        private Vector3 GetVectorAverage(Vector3[] vectors)
        {
            Vector3 averageVector = Vector3.zero;
            foreach (Vector3 vector in vectors) 
            {
                averageVector += vector;
            }
            averageVector /= vectors.Length;
            return averageVector;
        }

        private void MarioStoppedMoving()
        {
            Plugin.Settings.MarioPosition = transform.position;
        }

        IEnumerator CheckForLookDirection() 
        {
            Vector3 viewPos = _mainCamera.WorldToViewportPoint(transform.position);
            Vector3 camForwardFlat = _mainCamera.transform.forward;
            camForwardFlat = new Vector3(camForwardFlat.x, 0f, camForwardFlat.z).normalized;
            if (viewPos.x > 0.0f && viewPos.x < 1.0f && viewPos.y > 0.0f && viewPos.y < 1.0f && Vector3.Angle(camForwardFlat, -transform.forward) < 30f) {
                _sm64Mario.SetAction(SM64MarioAction.ACT_UNKNOWN_0002020E); // Wave action
                _waved = true;
            }
            yield return new WaitForSecondsRealtime(1f);
            if(_waved == false) StartCoroutine(CheckForLookDirection());
        }
    }
}
