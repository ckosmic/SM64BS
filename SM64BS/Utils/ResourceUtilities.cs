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
		internal AssetBundle mainBundle;

		public void Initialize() {
			mainBundle = LoadAssetBundleFromResource($"SM64BS.Resources.assets.unity3d");
		}

        public AssetBundle LoadAssetBundleFromResource(string path) {
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
