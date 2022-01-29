using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace SM64BS.Utils
{
	internal class ResourceUtilities : IInitializable
	{
		internal static string mainBundleResourcePath;

		private AssetBundle _mainBundle;

		public void Initialize()
		{
			if (mainBundleResourcePath != null && mainBundleResourcePath.Length > 0)
				LoadMainAssetBundleFromResource(mainBundleResourcePath);
		}

		public void LoadMainAssetBundleFromResource(string resourcePath)
		{
			_mainBundle = LoadAssetBundleFromResource(mainBundleResourcePath);
		}

		public T LoadAssetFromMainBundle<T>(string name) where T : UnityEngine.Object
		{
			return _mainBundle.LoadAsset<T>(name);
		}

        public AssetBundle LoadAssetBundleFromResource(string path)
		{
            var assembly = Assembly.GetExecutingAssembly();
			using (Stream stream = assembly.GetManifestResourceStream(path))
			{
				if (stream != null)
				{
					AssetBundle bundle = AssetBundle.LoadFromStream(stream);
					if (bundle == null)
					{
						Plugin.Log.Error("Failed to load asset bundle from: " + path);
					}
					return bundle;
				}
				else
				{
					Plugin.Log.Error("Failed to open asset bundle stream.");
					return null;
				}
			}
		}
    }
}
