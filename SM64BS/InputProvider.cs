using LibSM64;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SM64BS
{
    internal class InputProvider : SM64InputProvider
    {
        public Camera camera;

        public override Vector3 GetCameraLookDirection()
        {
            return camera.transform.forward;
        }

        public override Vector2 GetJoystickAxes()
        {
            return new Vector2(Input.GetAxis("HorizontalLeftHand"), -Input.GetAxis("VerticalLeftHand"));
        }

        public override bool GetButtonHeld(Button button)
        {
            switch (button) {
                case Button.Jump:
                    return OVRInput.Get(OVRInput.Button.One);
                case Button.Kick:
                    return OVRInput.Get(OVRInput.Button.Two);
                case Button.Stomp:
                    return OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) >= 0.5f;
            }
            
            return false;
        }
    }
}
