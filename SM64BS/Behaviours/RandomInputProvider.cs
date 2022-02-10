using LibSM64;
using UnityEngine;

namespace SM64BS.Behaviours
{
    internal class RandomInputProvider : SM64InputProvider
    {
        private int direction = 0;
        private float directionTimer = 0f;
        private float jumpTimer = 0f;
        private float punchTimer = 0f;
        private float stompTimer = 0f;

        private Vector2[] directions = { 
            new Vector2( 0f,  1f),
            new Vector2( 1f,  0f),
            new Vector2(-1f,  0f),
            new Vector2( 0f, -1f),
            new Vector2( 1f,  1f),
            new Vector2( 1f, -1f),
            new Vector2(-1f, -1f),
            new Vector2(-1f,  1f),
        };

        public override bool GetButtonHeld(Button button)
        {
            switch (button) {
                case Button.Jump:
                    if (Time.time > jumpTimer) { 
                        jumpTimer = Time.time + Random.Range(1f, 4f);
                        return true;
                    }
                    break;
                case Button.Kick:
                    if (Time.time > punchTimer) {
                        punchTimer = Time.time + Random.Range(3f, 10f);
                        return true;
                    }
                    break;
                case Button.Stomp:
                    if (Time.time > stompTimer) {
                        stompTimer = Time.time + Random.Range(4f, 10f);
                        return true;
                    }
                    break;
            }
            return false;
        }

        public override Vector3 GetCameraLookDirection()
        {
            return Vector3.forward;
        }

        public override Vector2 GetJoystickAxes()
        {
            if (Time.time > directionTimer) {
                direction = PickDirection(direction);
                directionTimer = Time.time + Random.Range(0.25f, 1f);
            }

            return directions[direction].normalized;
        }

        private int PickDirection(int exclusion) {
            int dir = Random.Range(0, directions.Length - 1);
            if (dir == exclusion)
                return PickDirection(exclusion);
            else
                return dir;
        }
    }
}
