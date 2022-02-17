using UnityEngine;
using UnityEngine.Events;

namespace SM64BS.Behaviours
{
    [AddComponentMenu("Saber Mario 64/Event Manager")]
    public class EventManager : MonoBehaviour
    {
        public bool[] sections = { false, false };

        public UnityEvent OnInitialize;
        public UnityEvent OnDispose;

        public UnityEvent OnNoteCut;
        public UnityEvent OnNoteMissed;
        public UnityEvent OnEnergyDidReach0;
    }
}
