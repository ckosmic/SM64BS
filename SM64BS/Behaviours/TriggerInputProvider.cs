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

        public override bool GetButtonHeld(Button button)
        {
            return false;
        }

        public override Vector3 GetCameraLookDirection()
        {
            return Vector3.forward;
        }

        public override Vector2 GetJoystickAxes()
        {
            UpdateTrigger();

            if (!_isTraveling) return Vector2.zero;

            Vector3 delta = _targetPosition - transform.position;

            if (delta.magnitude > 0.01f)
            {
                _sm64Mario.SetRotation(Quaternion.LookRotation(delta));
                return new Vector2(Mathf.Clamp01(delta.x), Mathf.Clamp01(delta.z));
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
            Utils.Types.VRControllerType priority = Utils.Types.VRControllerType.Left;
            if (GetVRButtonHeld(CommonUsages.triggerButton, Utils.Types.VRControllerType.Left))
            {
                isHeld = true;
                priority = Utils.Types.VRControllerType.Left;
            }
            if (GetVRButtonHeld(CommonUsages.triggerButton, Utils.Types.VRControllerType.Right))
            {
                isHeld = true;
                priority = Utils.Types.VRControllerType.Right;
            }

            if (!isHeld) return;

            Vector3 origin = GetVRHandPosition(priority);
            RaycastHit hit;
            if (Physics.Raycast(origin, -Vector3.up, out hit, 20f))
            {
                if (hit.transform.GetComponent<SM64StaticTerrain>() != null)
                {
                    SetTargetPosition(hit.point);
                }
            }
        }

        public void SetTargetPosition(Vector3 targetPosition)
        { 
            if(_sm64Mario == null)
                _sm64Mario = GetComponent<SM64Mario>();

            Vector3 direction = targetPosition - transform.position;
            _sm64Mario.SetRotation(Quaternion.LookRotation(direction));

            _targetPosition = targetPosition;
            _isTraveling = true;
        }
    }
}
