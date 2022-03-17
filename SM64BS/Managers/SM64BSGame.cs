using IPA.Utilities;
using LibSM64;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SM64BS.Managers
{
    public class SM64BSGame : MarioSceneManager
    {
        private readonly AppMarioManager _appMarioManager;
        private readonly SaberManager _saberManager;

        private Transform _saberLTransform, _saberRTransform, _cameraTransform = null;

        public Transform cameraTransform
        {
            get
            {
                if (_cameraTransform == null)
                    _cameraTransform = Camera.main.transform;
                return _cameraTransform;
            }
        }
        public Transform saberLTransform
        {
            get
            { 
                if(_saberLTransform == null)
                    _saberLTransform = _saberManager.leftSaber.GetField<Transform, Saber>("_handleTransform");
                return _saberLTransform;
            } 
        }
        public Transform saberRTransform
        {
            get
            {
                if (_saberRTransform == null)
                    _saberRTransform = _saberManager.rightSaber.GetField<Transform, Saber>("_handleTransform");
                return _saberRTransform;
            }
        }

        // Readonly smile
        internal SM64BSGame(AppMarioManager appMarioManager, SaberManager saberManager)
        {
            _appMarioManager = appMarioManager;
            _saberManager = saberManager;
        }

        public SM64Mario SpawnMario(Vector3 position, Quaternion rotation)
        {
            if (marios.Count < Plugin.Settings.MaxMarios)
            {
                SM64Context.RefreshStaticTerrain();
                SM64Mario newMario = _appMarioManager.SpawnMario(position, rotation);
                if(newMario != null)
                    marios.Add(newMario);
                return newMario;
            }
            return null;
        }
    }
}
