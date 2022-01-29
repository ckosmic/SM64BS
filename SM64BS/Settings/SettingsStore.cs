using IPA.Config.Stores;
using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;
using LibSM64;
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
        [UseConverter(typeof(ListConverter<Color32>))]
        public List<Color32> MarioColors = SM64Types.defaultMarioColors.ToList();
    }
}
