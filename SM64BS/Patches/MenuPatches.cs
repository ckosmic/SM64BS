using LibSM64;
using SiraUtil.Affinity;
using SM64BS.Managers;
using SM64BS.Plugins;
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
        [Inject] private BuiltInPluginLoader _builtInPluginLoader;

        [AffinityPostfix]
        [AffinityPatch(typeof(SettingsFlowCoordinator), nameof(SettingsFlowCoordinator.ApplySettings))]
        internal void Prefix()
        {
            Plugin.Log.Info("Applied settings");
            _builtInPluginLoader.UnloadPlugins();
            _appMarioManager.Dispose();
        }
    }
}
