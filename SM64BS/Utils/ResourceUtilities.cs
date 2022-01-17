using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SM64BS.Utils
{
    internal class ResourceUtilities
    {
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
