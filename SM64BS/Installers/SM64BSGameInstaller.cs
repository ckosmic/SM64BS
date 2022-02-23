using BeatSaberMarkupLanguage;
using IPA.Old;
using SM64BS.Behaviours;
using SM64BS.EventBroadcasters;
using SM64BS.Managers;
using SM64BS.Plugins;
using SM64BS.Plugins.BuiltIn;
using SM64BS.Plugins.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace SM64BS.Installers
{
    internal class SM64BSGameInstaller : Installer<SM64BSGameInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<SM64BSGame>().AsSingle();

            if (Plugin.Settings.SelectedPlugin.Length == 0 || Plugin.LoadedCustomPlugins.Count == 0) return;

            //BindBuiltInPlugin<PauseMenuMario>();
            //BindBuiltInPlugin<SpawnMarioOnMiss>();

            CustomPlugin[] customPlugins = new CustomPlugin[Plugin.LoadedCustomPlugins.Count];
            Plugin.LoadedCustomPlugins.Values.CopyTo(customPlugins, 0);
            CustomPlugin selectedPlugin = customPlugins.First(x => x.PluginId == Plugin.Settings.SelectedPlugin);
            BindCustomPlugin(selectedPlugin, selectedPlugin.MainType);

            Container.BindInterfacesTo<GameMarioManager>().AsSingle();

            Container.BindInterfacesAndSelfTo<PluginEventBroadcaster>().AsSingle();
            Container.BindInterfacesAndSelfTo<BeatmapEventBroadcaster>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnergyEventBroadcaster>().AsSingle();
            Container.BindInterfacesAndSelfTo<PauseEventBroadcaster>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScoreEventBroadcaster>().AsSingle();
        }

        private void BindBuiltInPlugin<T>() where T : IPluginEventHandler
        {
            if (typeof(T).BaseType == typeof(MonoBehaviour))
            {
                Container.BindInterfacesAndSelfTo<T>().FromComponentOnRoot().AsSingle();
            }
            else
            {
                Container.BindInterfacesAndSelfTo<T>().AsSingle();
            }
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
