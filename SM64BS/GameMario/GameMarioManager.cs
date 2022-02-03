using Zenject;
using UnityEngine;
using LibSM64;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using SM64BS.Behaviours;

namespace SM64BS.GameMario
{
    internal class GameMarioManager : IInitializable, IDisposable
    {
        private readonly MarioSpawner _marioSpawner;

        private List<GameObject> _marios;

        public GameMarioManager(MarioSpawner marioSpawner)
        {
            _marioSpawner = marioSpawner;
        }

        public void Dispose()
        {
            
        }

        public void Initialize()
        {
            SM64Context.Terminate();
            SM64Context.Initialize(Application.dataPath + "/../baserom.us.z64");
            SM64Context.SetScaleFactor(1.0f);

            GameObject groundGO = GameObject.CreatePrimitive(PrimitiveType.Plane);
            groundGO.AddComponent<SM64StaticTerrain>();
            groundGO.GetComponent<MeshRenderer>().enabled = false;
            groundGO.transform.position = new Vector3(0f, 0f, 2.25f);
            groundGO.transform.localScale = Vector3.one * 0.5f;

            _marios = new List<GameObject>();

            for (int i = 0; i < 5; i++) {
                GameObject marioGO = _marioSpawner.SpawnMario(groundGO.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0f, Random.Range(-0.5f, 0.5f)), Quaternion.Euler(0f, Random.Range(0f, 360f), 0f), false);
                UnityEngine.Object.DestroyImmediate(marioGO.GetComponent<InputProvider>());
                marioGO.AddComponent<RandomInputProvider>();
                _marios.Add(marioGO);
                _marioSpawner.InitializeMario(marioGO);
            }
        }
    }
}
