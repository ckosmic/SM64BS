using SM64BS.Behaviours;
using SM64BS.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SM64BS
{
    internal class MarioBundle
    {
        public BundleDescriptor descriptor;
        public GameObject rootPrefab { get; private set; } 

        private AssetBundle _bundle;

        public MarioBundle(string bundlePath, Sprite defaultIcon)
        {
            if (!string.IsNullOrEmpty(bundlePath))
            {
                _bundle = AssetBundle.LoadFromFile(bundlePath);
                ExtractBundle(_bundle);
                if (descriptor.icon == null)
                    descriptor.icon = defaultIcon;
            }
        }

        public MarioBundle(byte[] bundleData, Sprite defaultIcon)
        {
            if (bundleData.Length > 0)
            {
                _bundle = AssetBundle.LoadFromMemory(bundleData);
                ExtractBundle(_bundle);
                if (descriptor.icon == null)
                    descriptor.icon = defaultIcon;
            }
        }

        private void ExtractBundle(AssetBundle assetBundle)
        {
            if (assetBundle != null)
            {
                rootPrefab = assetBundle.LoadAsset<GameObject>("_SaberMario64");
                descriptor = rootPrefab.GetComponent<BundleDescriptor>();
                rootPrefab.gameObject.name = descriptor.bundleName;
                assetBundle.Unload(false);
            }
        }

        public void Dispose()
        {
            if (_bundle != null)
            {
                _bundle.Unload(true);
            }
            else
            {
                UnityEngine.Object.Destroy(descriptor);
            }
        }
    }
}
