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
	public class ResourceUtilities : IInitializable, IDisposable
	{
		internal static string MainBundleResourcePath { get; set; }
		internal static string MainBundleAssetsPath { get; set; } = "";
		internal static string RomPath { get { if (_validRomPath == null) _validRomPath = _romPathsToSearch.FirstOrDefault(x => File.Exists(x)); return _validRomPath; } }

		private AssetBundle _mainBundle;
		private static string[] _romPathsToSearch =
		{
			Path.Combine(Environment.CurrentDirectory, @"baserom.us.z64"),
			Path.Combine(Environment.CurrentDirectory, @"UserData\baserom.us.z64")
		};
		private static string _validRomPath = null;

		public void Initialize()
		{
			if (MainBundleResourcePath != null && MainBundleResourcePath.Length > 0)
				LoadMainAssetBundleFromResource(MainBundleResourcePath);
		}

		public void Dispose()
		{
			if(_mainBundle != null)
				_mainBundle.Unload(true);
		}

		public void LoadMainAssetBundleFromResource(string resourcePath)
		{
			_mainBundle = LoadAssetBundleFromResource(MainBundleResourcePath);
		}

		public T LoadAssetFromMainBundle<T>(string name) where T : UnityEngine.Object
		{
			return _mainBundle.LoadAsset<T>(MainBundleAssetsPath + name);
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
			Texture2D tex = LoadTextureFromResource(path);
			if (tex == null) return null;
			Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0), 100);
			return sprite;
		}

		public Texture2D LoadTextureFromResource(string path)
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
						return tex;
					}
					else
					{
						Plugin.Log.Error("Failed to load texture.");
						return null;
					}
				}
				else
				{
					Plugin.Log.Error("Failed to open texture resource stream.");
					return null;
				}
			}
		}
	}
}
