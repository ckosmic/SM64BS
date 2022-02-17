namespace SM64BS.Utils
{
    public class Types
    {
        public enum VRControllerType
        { 
            None,
            Left,
            Right,
            GameController
        }

        public struct GrabState
        { 
            public VRControllerType controllerType;
            public bool isGrabbing;
        }
    }
}
