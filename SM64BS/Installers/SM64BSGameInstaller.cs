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

            foreach (CustomPlugin plugin in Plugin.LoadedCustomPlugins.Values)
            {
                BindCustomPlugin(plugin, plugin.MainType);
            }

            Container.BindInterfacesTo<GameMarioManager>().AsSingle();

            Container.BindInterfacesAndSelfTo<PluginEventBroadcaster>().AsSingle();
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
