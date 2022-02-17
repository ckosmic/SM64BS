using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SM64BS.Utils
{
    internal class BundleLoader
    {
        private const string BUNDLE_DIRECTORY = "UserData\\SM64BS\\Bundles";
        private readonly ResourceUtilities _utils;

        private Sprite _defaultIcon;

        public bool isLoaded { get; private set; } = false;
        public int selectedBundleIndex { get; internal set; } = 0;
        public List<MarioBundle> marioBundles { get; private set; } = new List<MarioBundle>();
        public List<string> assetBundlePaths { get; private set; } = new List<string>();

        public BundleLoader(ResourceUtilities utils)
        {
            _utils = utils;
            GetBundlesPath();
        }

        internal void Load()
        {
            if (!isLoaded)
            {
                string bundlePath = GetBundlesPath();

                _defaultIcon = _utils.LoadSpriteFromResource("SM64BS.Resources.icon.png");

                string[] bundlePaths = Directory.GetFiles(bundlePath, "*.sm64bs");
                LoadMarioBundles(bundlePaths);

                if(Plugin.Settings.SelectedBundle != "default")
                {
                    selectedBundleIndex = marioBundles.FindIndex(x => x.descriptor.bundleId == Plugin.Settings.SelectedBundle);
                }

                isLoaded = true;
            }
        }

        internal void Clear()
        {
            for(int i = 0; i < marioBundles.Count; i++)
            {
                marioBundles[i].Dispose();
                marioBundles[i] = null;
            }

            isLoaded = false;
            selectedBundleIndex = 0;
            marioBundles = new List<MarioBundle>();
        }

        internal void Reload()
        {
            Clear();
            Load();
        }

        public void LoadMarioBundles(string[] bundlePaths)
        {
            MarioBundle emptyBundle = new MarioBundle("", _defaultIcon);
            emptyBundle.descriptor = new Behaviours.BundleDescriptor();
            emptyBundle.descriptor.bundleName = "Nothing";
            emptyBundle.descriptor.author = "";
            emptyBundle.descriptor.description = "Disable Mario in the game scene.";
            emptyBundle.descriptor.bundleId = "default";
            emptyBundle.descriptor.icon = _defaultIcon;

            assetBundlePaths.Add("");
            marioBundles.Add(emptyBundle);

            foreach (string path in bundlePaths)
            {
                MarioBundle bundle = new MarioBundle(path, _defaultIcon);
                if (bundle != null)
                {
                    assetBundlePaths.Add(path);
                    marioBundles.Add(bundle);
                }
            }
        }

        public MarioBundle GetBundleById(string bundleId)
        { 
            return marioBundles.Find(x => x.descriptor.bundleId == bundleId);
        }

        private string GetBundlesPath()
        {
            string bundlePath = Path.Combine(Environment.CurrentDirectory, BUNDLE_DIRECTORY);
            Directory.CreateDirectory(bundlePath);

            return bundlePath;
        }
    }
}
