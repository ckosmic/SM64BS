using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM64BS.Plugins
{
    internal class CustomPlugin
    {
        [JsonProperty(nameof(Name), Required = Required.Always)]
        public string Name;
        [JsonProperty(nameof(Description), Required = Required.Always)]
        public string Description;
        [JsonProperty(nameof(BundleId), Required = Required.Always)]
        public string BundleId;
        [JsonProperty(nameof(MainClass), Required = Required.Always)]
        internal string MainClass;

        public Type MainType;
    }
}
