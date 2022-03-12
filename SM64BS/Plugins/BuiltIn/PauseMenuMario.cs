using LibSM64;
using SM64BS.Attributes;
using SM64BS.Behaviours;
using SM64BS.Plugins.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SM64BS.Plugins.BuiltIn
{
    [PluginMetadata(Name = "Pause Menu Mario", Author = "ckosmic", Description = "A Mario for your pause menu.", PluginId = "com.christiankosman.pausemenumario")]
    internal class PauseMenuMario : SM64BSPlugin, IPauseEventHandler
    {
        private GameObject _marioGo;
        private SM64Mario _sm64Mario;

        public override void PluginInitialize()
        {
            SM64Context.SetScaleFactor(2.0f);

            GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
            ground.AddComponent<SM64StaticTerrain>();
            ground.GetComponent<MeshRenderer>().enabled = false;
            ground.transform.position = new Vector3(0, 0.02f, 0f);
            ground.transform.localScale = new Vector3(0.3f, 1f, 0.2f);

            _sm64Mario = GameScene.SpawnMario(new Vector3(1, 0.02f, 0.75f), Quaternion.Euler(0, 180, 0));
            TriggerInputProvider inputProvider = _sm64Mario.gameObject.AddComponent<TriggerInputProvider>();
            inputProvider.SetSaberTransforms(GameScene.saberLTransform, GameScene.saberRTransform);
            _sm64Mario.RefreshInputProvider();
            _sm64Mario.marioRendererObject.SetActive(false);
            _sm64Mario.enabled = false;
        }

        public override void PluginDispose()
        {
            
        }

        public void DidPause()
        {
            _sm64Mario.marioRendererObject.SetActive(true);
            _sm64Mario.enabled = true;
        }

        public void DidUnpause()
        {
            _sm64Mario.marioRendererObject.SetActive(false);
            _sm64Mario.enabled = false;
        }
    }
}
