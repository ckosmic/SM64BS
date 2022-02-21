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
    internal class GameMarioManager : MarioSceneManager, ILateDisposable
    {
        private readonly AppMarioManager _appMarioManager;

        public GameMarioManager(AppMarioManager appMarioManager, ScoreController scoreController, GameEnergyCounter gameEnergyCounter)
{
            _appMarioManager = appMarioManager;
        }

        public override void Initialize()
        {
            base.Initialize();

            _appMarioManager.CreateMenuBufferPlatform();
            _appMarioManager.SetMenuTerrainsEnabled(false);
        }

        public void LateDispose()
        {
            _appMarioManager.SetMenuTerrainsEnabled(true);
            SM64Context.SetScaleFactor(2.0f);
        }
    }
}
