using UnityEngine;

namespace SM64BS.Behaviours
{
    [AddComponentMenu("Saber Mario 64/Bundle Descriptor")]
    public class BundleDescriptor : MonoBehaviour
    {
        public string bundleName = "MyMarioBundle";
        public string author = "unknown";
        [TextArea(3, 5)]
        public string description = "A Saber Mario 64 bundle";
        public Sprite icon;

        [HideInInspector]
        public string bundleId = "unknown.MyMarioBundle";
    }
}
