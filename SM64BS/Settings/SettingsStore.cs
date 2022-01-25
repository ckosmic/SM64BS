using IPA.Config.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace SM64BS.Settings
{
    internal class SettingsStore
    {
        public Vector3 MarioPosition = new Vector3(5f, 0f, 5f);
        public string MarioName = "Mario";
        public bool ShowNamePlate = true;
    }
}
