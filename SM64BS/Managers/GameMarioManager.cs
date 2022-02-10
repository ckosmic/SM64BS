using Zenject;
using UnityEngine;
using LibSM64;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using SM64BS.Behaviours;
using static BeatmapSaveData;

namespace SM64BS
{
    internal class GameMarioManager : IInitializable, IDisposable
    {
        private readonly AppMarioManager _appMarioManager;
        private readonly ScoreController _scoreController;

        private List<GameObject> _marios;
        private GameObject _groundGO;

        public GameMarioManager(AppMarioManager appMarioManager, ScoreController scoreController)
        {
            _appMarioManager = appMarioManager;
            _scoreController = scoreController;
        }

        public void Initialize()
        {
            if (Plugin.Settings.SpawnMarioOnMiss)
                _scoreController.noteWasMissedEvent += NoteMissedHandler;

            _groundGO = GameObject.CreatePrimitive(PrimitiveType.Plane);
            _groundGO.AddComponent<SM64StaticTerrain>();
            _groundGO.GetComponent<MeshRenderer>().enabled = false;
            _groundGO.transform.position = new Vector3(0f, 0f, 2.25f);
            _groundGO.transform.localScale = Vector3.one * 0.5f;

            _appMarioManager.SetMenuTerrainsEnabled(false);

            _marios = new List<GameObject>();
        }

        public void Dispose()
        {
            if (Plugin.Settings.SpawnMarioOnMiss)
                _scoreController.noteWasMissedEvent -= NoteMissedHandler;

            UnityEngine.Object.DestroyImmediate(_groundGO);
            foreach (GameObject marioGO in _marios)
            {
                marioGO.GetComponent<SM64Mario>().Terminate();
            }

            _appMarioManager.SetMenuTerrainsEnabled(true);
        }

        private void NoteMissedHandler(NoteData a1, int a2)
        {
            if (_marios.Count < Plugin.Settings.MaxMarios)
            {
                GameObject marioGO = _appMarioManager.SpawnMario(_groundGO.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0f, Random.Range(-0.5f, 0.5f)), Quaternion.Euler(0f, Random.Range(0f, 360f), 0f), false);
                UnityEngine.Object.DestroyImmediate(marioGO.GetComponent<InputProvider>());
                marioGO.AddComponent<RandomInputProvider>();
                _marios.Add(marioGO);
                _appMarioManager.InitializeMario(marioGO);
            }
        }
    }
}
