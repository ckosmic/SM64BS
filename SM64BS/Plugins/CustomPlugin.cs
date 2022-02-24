using Newtonsoft.Json;
using SM64BS.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SM64BS.Plugins
{
    internal class CustomPlugin
    {
        [JsonProperty(nameof(Name), Required = Required.Always)]
        public string Name;
        [JsonProperty(nameof(Author), Required = Required.Always)]
        public string Author;
        [JsonProperty(nameof(Description), Required = Required.DisallowNull)]
        public string Description;
        [JsonProperty(nameof(PluginId), Required = Required.Always)]
        public string PluginId;
        [JsonProperty(nameof(MainClass), Required = Required.Always)]
        internal string MainClass;

        public Type MainType;

        public CustomPlugin()
        {

        }

        public CustomPlugin(Assembly assembly, PluginMetadataAttribute metadataAttribute)
        { 
            Name = metadataAttribute.Name;
            Author = metadataAttribute.Author;
            Description = metadataAttribute.Description;
            PluginId = metadataAttribute.PluginId;
            MainClass = metadataAttribute.MainClass;
            MainType = assembly.GetType(MainClass);
        }
    }
}
