using LibSM64;
using SiraUtil.Affinity;
using SM64BS.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace SM64BS.Patches
{
    internal class MenuPatches : IAffinity
    {
        [Inject] private AppMarioManager _appMarioManager;

        [AffinityPostfix]
        [AffinityPatch(typeof(SettingsFlowCoordinator), nameof(SettingsFlowCoordinator.ApplySettings))]
        internal void Prefix()
        {
            Plugin.Log.Info("Applied settings");
            _appMarioManager.Dispose();
        }
    }
}
