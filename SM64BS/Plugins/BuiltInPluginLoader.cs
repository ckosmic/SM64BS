using SM64BS.Attributes;
using SM64BS.Plugins.BuiltIn;
using SM64BS.Plugins.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace SM64BS.Plugins
{
    internal class BuiltInPluginLoader : IInitializable
    {

        public void Initialize()
        {
            Plugin.LoadedCustomPlugins.Clear();

            AddPlugin<PauseMenuMario>();
            AddPlugin<SpawnMarioOnMiss>();
        }

        public void AddPlugin<T>() where T : IPluginEventHandler
        {
            PluginMetadataAttribute metadataAttribute = typeof(T).GetCustomAttribute<PluginMetadataAttribute>();
            if (metadataAttribute != null)
            {
                metadataAttribute.MainClass = typeof(T).FullName;
                Assembly mainAssembly = Assembly.GetExecutingAssembly();
                CustomPlugin plugin = new CustomPlugin(mainAssembly, metadataAttribute);
                Tuple<Assembly, int> t = new Tuple<Assembly, int>(mainAssembly, Plugin.LoadedCustomPlugins.Count);
                Plugin.LoadedCustomPlugins.Add(t, plugin);
            }
            else 
            {
                Plugin.Log.Info(typeof(T).FullName + " has no metadata attribute.");
            }
        }
    }
}
