using LibSM64;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SM64BS.Behaviours
{
    internal class MarioColorManager : MonoBehaviour
    {
        private SM64Mario _sm64Mario;

        private void Start()
        {
            _sm64Mario = GetComponent<SM64Mario>();
            SetMarioColors(Plugin.Settings.MarioColors.ToArray());
        }

        public void SetMarioColors(Color32[] colors)
        {
            _sm64Mario.SetColors(colors);
        }
    }
}
