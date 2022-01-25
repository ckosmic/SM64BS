﻿using Zenject;
using UnityEngine;
using LibSM64;
using System;
using SM64BS.Behaviours;
using SM64BS.Utils;

namespace SM64BS
{
    internal class MenuMarioManager : IInitializable, IDisposable
    {
        private readonly MarioSpawner _marioSpawner;
        private readonly ResourceUtilities _utils;


        private GameObject _mario;
        private NamePlate _namePlate;

        public MenuMarioManager(MarioSpawner marioSpawner, ResourceUtilities utils) {
            _marioSpawner = marioSpawner;
            _utils = utils;
        }

        public void Initialize() {
            SM64Context.Terminate();
            SM64Context.Initialize(Application.dataPath + "/../baserom.us.z64");
            SM64Context.SetScaleFactor(2.0f);

            GameObject groundGO = GameObject.CreatePrimitive(PrimitiveType.Plane);
            groundGO.AddComponent<SM64StaticTerrain>();
            groundGO.GetComponent<MeshRenderer>().enabled = false;
            groundGO.transform.localScale = Vector3.one * 5f;

            Vector3 spawnPos = Plugin.Settings.MarioPosition;

            _mario = _marioSpawner.SpawnMario(spawnPos, Quaternion.LookRotation(new Vector3(0, spawnPos.y, 0) - spawnPos));
            _mario.AddComponent<MarioBehaviour>();

            if (Plugin.Settings.ShowNamePlate)
            {
                _namePlate = new GameObject("NamePlate").AddComponent<NamePlate>();
                _namePlate.transform.SetParent(_mario.transform);
                _namePlate.transform.localPosition = new Vector3(0f, 0.9f, 0f);
                _namePlate.Initialize(_utils);
            }
        }

        public void Dispose()
        {
            
        }
    }
}
