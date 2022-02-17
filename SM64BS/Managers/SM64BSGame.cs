using LibSM64;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SM64BS.Managers
{
    public class SM64BSGame
    {
        private AppMarioManager _appMarioManager;

        internal SM64BSGame(AppMarioManager appMarioManager)
        {
            _appMarioManager = appMarioManager;
        }

        public SM64Mario SpawnMario(Vector3 position, Quaternion rotation)
        {
            SM64Context.RefreshStaticTerrain();
            return _appMarioManager.SpawnMario(position, rotation);
        }
    }
}
