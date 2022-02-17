using Zenject;
using UnityEngine;
using LibSM64;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using SM64BS.Behaviours;
using static BeatmapSaveData;
using SM64BS.Utils;
using IPA.Utilities;

namespace SM64BS.Managers
{
    internal class GameMarioManager : IInitializable, IDisposable, ILateDisposable
    {
        private readonly AppMarioManager _appMarioManager;

        private List<GameObject> _marios;
        private GameObject _bundleGO;

        public GameMarioManager(AppMarioManager appMarioManager, ScoreController scoreController, GameEnergyCounter gameEnergyCounter)
{
            _appMarioManager = appMarioManager;
        }

        public void Initialize()
        {

            // TODO: Instantiate mario

            _appMarioManager.CreateMenuBufferPlatform();
            _appMarioManager.SetMenuTerrainsEnabled(false);

            _marios = new List<GameObject>();
        }

        public void Dispose()
        {
            foreach (GameObject marioGO in _marios)
            {
                UnityEngine.Object.DestroyImmediate(marioGO);
            }
        }

        public void LateDispose()
        {
            UnityEngine.Object.DestroyImmediate(_bundleGO);
            _appMarioManager.SetMenuTerrainsEnabled(true);
        }
    }
}
