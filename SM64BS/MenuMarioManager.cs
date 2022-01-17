using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;
using UnityEngine;
using LibSM64;

namespace SM64BS
{
    internal class MenuMarioManager : IInitializable
    {
        private readonly MarioSpawner _marioSpawner;
        
        private GameObject _mario;

        public MenuMarioManager(MarioSpawner marioSpawner) {
            _marioSpawner = marioSpawner;
        }

        public void Initialize() {
            GameObject groundGO = GameObject.CreatePrimitive(PrimitiveType.Plane);
            groundGO.AddComponent<SM64StaticTerrain>();
            groundGO.GetComponent<MeshRenderer>().enabled = false;
            groundGO.transform.localScale = Vector3.one * 5f;

            _mario = _marioSpawner.SpawnMario(Vector3.zero, Quaternion.identity);
        }
    }
}
