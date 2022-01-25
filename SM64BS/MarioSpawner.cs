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

        private Shader _marioShader;
        private Material _marioMaterial;

        public MarioSpawner(ResourceUtilities utils) {
            _utils = utils;
        }

        public GameObject SpawnMario(Vector3 position, Quaternion rotation, bool initialize = true) {
            GameObject marioGO = new GameObject("Mario");

            Transform marioTransform = marioGO.transform;
            marioTransform.position = position;
            marioTransform.rotation = rotation;

            InputProvider inputProvider = marioGO.AddComponent<InputProvider>();
            inputProvider.camera = Camera.main;

            SM64Mario sm64Mario = marioGO.AddComponent<SM64Mario>();

            if (_marioShader == null)
            {
                _marioShader = _utils.mainBundle.LoadAsset<Shader>("Assets/SM64BS/mario.shader");
            }

            if (_marioMaterial == null)
            {
                _marioMaterial = new Material(_marioShader);
                _marioMaterial.SetFloat("_Ambient", 0.25f);
            }
            sm64Mario.SetField("material", _marioMaterial);

            if (initialize) sm64Mario.Initialize();

            return marioGO;
        }

        public void InitializeMario(GameObject marioGO) {
            marioGO.GetComponent<SM64Mario>().Initialize();
        }
    }
}
