using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM64BS.Utils
{
    internal class Types
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
