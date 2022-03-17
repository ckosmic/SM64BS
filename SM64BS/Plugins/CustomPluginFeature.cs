using IPA.Loader;
using IPA.Loader.Features;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SM64BS.Plugins
{
    // Absolutely yoinked this from Counters+'s CustomCounterFeature
    public class CustomPluginFeature : Feature
    {
        private Dictionary<PluginMetadata, CustomPlugin> customPlugins = new Dictionary<PluginMetadata, CustomPlugin>();

        protected override bool Initialize(PluginMetadata meta, JObject featureData)
        {
            CustomPlugin plugin;
            try
            {
                plugin = featureData.ToObject<CustomPlugin>();
            }
            catch (Exception ex)
            {
                InvalidMessage = $"Invalid data: { ex }";
                return false;
            }

            customPlugins.Add(meta, plugin);
            return true;
        }

        public override void AfterInit(PluginMetadata meta)
        {
            if (customPlugins.TryGetValue(meta, out CustomPlugin plugin))
            {
                if (!TryLoadType(ref plugin.MainType, meta, plugin.MainClass))
                {
                    Plugin.Log.Error($"Failed to load a Type from the provided MainClass for { plugin.Name }.");
                    return;
                }

                if (plugin.PluginId == Plugin.Settings.SelectedPlugin)
                {
                    Plugin.Settings.SelectedPluginIndex = Plugin.LoadedCustomPlugins.Count;
                }

                Tuple<Assembly, int> t = new Tuple<Assembly, int>(meta.Assembly, Plugin.LoadedCustomPlugins.Count);
                Plugin.Log.Info("Loaded " + plugin.Name);
                Plugin.LoadedCustomPlugins.Add(t, plugin);
            }
        }

        private bool TryLoadType(ref Type typeToLoad, PluginMetadata meta, string location)
        {
            // Totally didn't yoink this from Counters+'s CustomCounterFeature that yoinked this from BSIPA's ConfigProviderFeature
            try
            {
                typeToLoad = meta.Assembly.GetType(location);
            }
            catch (ArgumentException)
            {
                InvalidMessage = $"Invalid type name {location}";
                return false;
            }
            catch (Exception e) when (e is FileNotFoundException || e is FileLoadException || e is BadImageFormatException)
            {
                string filename;

                switch (e)
                {
                    case FileNotFoundException fn:
                        filename = fn.FileName;
                        goto hasFilename;
                    case FileLoadException fl:
                        filename = fl.FileName;
                        goto hasFilename;
                    case BadImageFormatException bi:
                        filename = bi.FileName;
                    hasFilename:
                        InvalidMessage = $"Could not find {filename} while loading type";
                        break;
                    default:
                        InvalidMessage = $"Error while loading type: {e}";
                        break;
                }

                return false;
            }
            catch (Exception e) // Is this unnecessary? Maybe.
            {
                InvalidMessage = $"An unknown error occured: {e}";
                return false;
            }

            return true;
        }
    }
}
