using IPA.Utilities;
using LibSM64;
using SM64BS.Behaviours;
using SM64BS.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace SM64BS.Managers
{
    internal class AppMarioManager : IInitializable, IDisposable
    {
        private readonly ResourceUtilities _utils;

        private Shader _marioShader;
        private Material _marioMaterial;
        private List<MonoBehaviour> _menuTerrains = new List<MonoBehaviour>();
        private GameObject _menuBufferPlatform;

        public GameObject menuMarioGO;

        public AppMarioManager(ResourceUtilities utils)
        {
            _utils = utils;
        }

        public void Initialize()
        {
            SM64Context.Initialize(Path.Combine(Environment.CurrentDirectory, "baserom.us.z64"));
            SM64Context.SetScaleFactor(2.0f);
        }

        public void Dispose()
        {
            SM64Context.Terminate();
        }

        public SM64Mario SpawnMario(Vector3 position, Quaternion rotation)
        {
            GameObject marioGO = new GameObject("Mario");

            Transform marioTransform = marioGO.transform;
            marioTransform.position = position;
            marioTransform.rotation = rotation;

            SM64Mario sm64Mario = marioGO.AddComponent<SM64Mario>();

            if (_marioShader == null)
            {
                _marioShader = _utils.LoadAssetFromMainBundle<Shader>("Assets/SM64BS/mario.shader");
            }

            if (_marioMaterial == null)
            {
                _marioMaterial = new Material(_marioShader);
                _marioMaterial.SetFloat("_Ambient", 0.25f);
            }
            sm64Mario.SetField("material", _marioMaterial);

            if (!sm64Mario.Initialize())
                return null;

            return sm64Mario;
        }

        public void AddMenuTerrain(SM64StaticTerrain terrain)
        {
            _menuTerrains.Add(terrain);
        }

        public void AddMenuTerrain(SM64DynamicTerrain terrain)
        {
            _menuTerrains.Add(terrain);
        }

        public void SetMenuTerrainsEnabled(bool enabled)
        {
            foreach (MonoBehaviour mb in _menuTerrains)
            {
                mb.enabled = enabled;
            }
            SM64Context.RefreshStaticTerrain();
        }

        // Need this or else literally everything breaks really badly
        public void CreateMenuBufferPlatform()
        {
            if(_menuBufferPlatform != null) UnityEngine.Object.DestroyImmediate(_menuBufferPlatform);

            _menuBufferPlatform = GameObject.CreatePrimitive(PrimitiveType.Quad);
            UnityEngine.Object.DestroyImmediate(_menuBufferPlatform.GetComponent<MeshCollider>());
            _menuBufferPlatform.AddComponent<SM64StaticTerrain>();
            _menuBufferPlatform.GetComponent<MeshRenderer>().enabled = false;
            _menuBufferPlatform.transform.position = menuMarioGO.transform.position;
            _menuBufferPlatform.transform.localScale = Vector3.one * 0.5f;
            _menuBufferPlatform.transform.rotation = Quaternion.Euler(90, 0, 0);
            _menuBufferPlatform.name = "MenuMarioPlatform";
            UnityEngine.Object.DontDestroyOnLoad(_menuBufferPlatform);
        }
    }
}
