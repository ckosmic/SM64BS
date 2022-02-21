using LibSM64;
using System;
using System.Collections.Generic;
using Zenject;

namespace SM64BS.Managers
{
    internal class MarioSceneManager : IInitializable, IDisposable
    {
        protected List<SM64Mario> marios = new List<SM64Mario>();

        public virtual void Initialize()
        {
            marios = new List<SM64Mario>();
        }

        public virtual void Dispose()
        {
            foreach (SM64Mario mario in marios)
            {
                UnityEngine.Object.DestroyImmediate(mario.gameObject);
            }
        }
    }
}
