using BeatSaberMarkupLanguage;
using SM64BS.Behaviours;
using SM64BS.EventBroadcasters;
using SM64BS.Managers;
using SM64BS.Plugins;
using System;
using UnityEngine;
using Zenject;

namespace SM64BS.Installers
{
    internal class SM64BSGameInstaller : Installer<SM64BSGameInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<SM64BSGame>().AsSingle();

            if (Plugin.Settings.SelectedBundle.Length > 0)
            {
                foreach (CustomPlugin plugin in Plugin.LoadedCustomPlugins.Values)
                {
                    if (plugin.BundleId == Plugin.Settings.SelectedBundle)
                    {
                        BindCustomPlugin(plugin, plugin.MainType);
                    }
                }
            }

            Container.BindInterfacesTo<GameMarioManager>().AsSingle();

            Container.BindInterfacesAndSelfTo<PluginEventBroadcaster>().AsSingle();
            Container.BindInterfacesAndSelfTo<BeatmapEventBroadcaster>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnergyEventBroadcaster>().AsSingle();
            Container.BindInterfacesAndSelfTo<PauseEventBroadcaster>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScoreEventBroadcaster>().AsSingle();
        }

        private void BindCustomPlugin(CustomPlugin plugin, Type pluginMainType)
        {
            if (pluginMainType == typeof(MonoBehaviour))
            {
                Container.BindInterfacesAndSelfTo(pluginMainType).FromComponentOnRoot().AsSingle();
            }
            else
            {
                Container.BindInterfacesAndSelfTo(pluginMainType).AsSingle();
            }
        }
    }
}
