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

		public Sprite LoadSpriteFromResource(string path)
		{
			Assembly assembly = Assembly.GetCallingAssembly();
			using (Stream stream = assembly.GetManifestResourceStream(path))
			{
				if (stream != null)
				{
					byte[] data = new byte[stream.Length];
					stream.Read(data, 0, (int)stream.Length);
					Texture2D tex = new Texture2D(2, 2);
					if (tex.LoadImage(data))
					{
						Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0), 100);
						return sprite;
					}
				}
				else
				{
					Plugin.Log.Error("Failed to open sprite resource stream.");
					return null;
				}
			}
			return null;
		}
	}
}
