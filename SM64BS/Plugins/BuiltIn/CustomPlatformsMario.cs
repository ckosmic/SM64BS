using LibSM64;
using SM64BS.Attributes;
using SM64BS.Behaviours;
using SM64BS.Managers;
using SM64BS.Plugins.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace SM64BS.Plugins.BuiltIn
{
    [PluginMetadata(Name = "Custom Platforms Mario", Author = "ckosmic", Description = "Enables collisions in Custom Platforms for Mario to run around in.", PluginId = "com.christiankosman.customplatformsmario")]
    internal class CustomPlatformsMario : SM64BSPlugin
    {
        private SM64Mario _sm64Mario;

        public override void PluginInitialize()
        {
            SM64Context.SetScaleFactor(2.0f);
        }

        public override void PluginDispose()
        {
            
        }

        IEnumerator WaitForPlatform()
        {
            yield return new WaitUntil(() => GameObject.Find("CustomPlatforms"));

            Transform platform = GameObject.Find("CustomPlatforms")?.transform;
            if (platform == null) yield return null;
            foreach (MeshFilter filter in platform.GetComponentsInChildren<MeshFilter>(false))
            {
                filter.gameObject.AddComponent<SM64StaticTerrain>();
            }
            SM64Context.RefreshStaticTerrain();

            _sm64Mario = GameScene.SpawnMario(new Vector3(1, 0.02f, 0.75f), Quaternion.Euler(0, 180, 0));
            _sm64Mario.gameObject.AddComponent<VRInputProvider>();
            _sm64Mario.RefreshInputProvider();
        }
    }
}
