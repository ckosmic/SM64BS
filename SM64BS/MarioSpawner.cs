using IPA.Utilities;
using LibSM64;
using SM64BS.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SM64BS
{
    internal class MarioSpawner
    {
        private readonly ResourceUtilities _utils;
        private readonly IVRPlatformHelper _vrPlatformHelper;

        public MarioSpawner(ResourceUtilities utils, IVRPlatformHelper vRPlatformHelper) {
            _utils = utils;
            _vrPlatformHelper = vRPlatformHelper;
        }

        public GameObject SpawnMario(Vector3 position, Quaternion rotation) {
            GameObject marioGO = new GameObject("Mario");
            Transform marioTransform = marioGO.transform;
            marioTransform.position = position;
            marioTransform.rotation = rotation;

            InputProvider inputProvider = marioGO.AddComponent<InputProvider>();
            inputProvider.camera = Camera.main;
            inputProvider.vRPlatformHelper = _vrPlatformHelper;

            SM64Mario sm64Mario = marioGO.AddComponent<SM64Mario>();
            sm64Mario.enabled = false;

            AssetBundle assets = _utils.LoadAssetBundleFromResource($"SM64BS.Resources.assets.unity3d");
            Shader marioShader = assets.LoadAsset<Shader>("Assets/SM64BS/mario.shader");

            Material marioMaterial = new Material(marioShader);
            marioMaterial.SetFloat("_Ambient", 0.25f);
            sm64Mario.SetField("material", marioMaterial);

            sm64Mario.enabled = true;

            return marioGO;
        }
    }
}
