using LibSM64;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;
using Zenject;

namespace SM64BS
{
    internal class InputProvider : SM64InputProvider
    {
        internal Camera camera;
        internal IVRPlatformHelper vRPlatformHelper;
        internal List<InputDevice> leftControllers;
        internal List<InputDevice> rightControllers;

        public void Start()
        {
            leftControllers = new List<InputDevice>();
            rightControllers = new List<InputDevice>();

            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left, leftControllers);
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, rightControllers);
        }

        public override Vector3 GetCameraLookDirection()
        {
            return camera.transform.forward;
        }

        public override Vector2 GetJoystickAxes()
        {
            Vector2 axis = Vector2.zero;
            if(leftControllers.Count > 0)
                leftControllers[0].TryGetFeatureValue(CommonUsages.primary2DAxis, out axis);
            return axis;
        }

        public override bool GetButtonHeld(Button button)
        {
            bool held = false;
            if (leftControllers.Count > 0 && rightControllers.Count > 0)
            {
                switch (button)
                {
                    case Button.Jump:
                        rightControllers[0].TryGetFeatureValue(CommonUsages.primaryButton, out held);
                        break;
                    case Button.Kick:
                        rightControllers[0].TryGetFeatureValue(CommonUsages.secondaryButton, out held);
                        break;
                    case Button.Stomp:
                        float triggerValue = 0.0f;
                        leftControllers[0].TryGetFeatureValue(CommonUsages.trigger, out triggerValue);
                        held = triggerValue >= 0.5f;
                        break;
                }
            }
            return held;
        }
    }
}
