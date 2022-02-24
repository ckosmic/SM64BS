using LibSM64;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR;
using SM64BS.Utils;

namespace SM64BS.Behaviours
{
    public class TriggerInputProvider : VRInputProvider
    {
        private Vector3 _targetPosition;
        private SM64Mario _sm64Mario;
        private bool _isTraveling = false;
        private Transform _saberLTransform, _saberRTransform;

        public override bool GetButtonHeld(Button button)
        {
            return false;
        }

        public override Vector3 GetCameraLookDirection()
        {
            return transform.forward;
        }

        public override Vector2 GetJoystickAxes()
        {
            UpdateTrigger();

            if (!_isTraveling) return Vector2.zero;

            Vector3 delta = _targetPosition - transform.position;
            delta.y = 0;

            if (Mathf.Abs(delta.magnitude) >= 0.1f)
            {
                _sm64Mario.SetRotation(Quaternion.LookRotation(delta));
                return new Vector2(0.0f, 1.0f);
            }
            else
            {
                _isTraveling = false;
                _sm64Mario.SetPosition(_targetPosition);
                return Vector2.zero;
            }
        }

        private void UpdateTrigger()
        {
            bool isHeld = false;
            Utils.Types.VRControllerType hand = Utils.Types.VRControllerType.Left;
            if (GetVRButtonHeld(CommonUsages.triggerButton, Utils.Types.VRControllerType.Left))
            {
                isHeld = true;
                hand = Utils.Types.VRControllerType.Left;
            }
            if (GetVRButtonHeld(CommonUsages.triggerButton, Utils.Types.VRControllerType.Right))
            {
                isHeld = true;
                hand = Utils.Types.VRControllerType.Right;
            }

            if (!isHeld) return;

            Transform saberTransform = hand == Utils.Types.VRControllerType.Left ? _saberLTransform : _saberRTransform;

            Vector3 origin = GetVRHandPosition(hand);
            RaycastHit hit;
            if (Physics.Raycast(origin, saberTransform.forward, out hit, 20f))
            {
                if (hit.transform.GetComponent<SM64StaticTerrain>() != null && Vector3.Distance(transform.position, hit.point) > 0.3f)
                {
                    SetTargetPosition(hit.point);
                }
            }
        }

        public void SetTargetPosition(Vector3 targetPosition)
        { 
            if(_sm64Mario == null)
                _sm64Mario = GetComponent<SM64Mario>();

            Vector3 delta = targetPosition - transform.position;
            _sm64Mario.SetRotation(Quaternion.LookRotation(delta));

            _targetPosition = targetPosition;
            _isTraveling = true;
        }

        public void SetSaberTransforms(Transform saberL, Transform saberR)
        {
            _saberLTransform = saberL;
            _saberRTransform = saberR;
        }
    }
}
