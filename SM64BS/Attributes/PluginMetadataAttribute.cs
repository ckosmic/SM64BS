using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SM64BS.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class PluginMetadataAttribute : Attribute
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string PluginId { get; set; }
        public string MainClass { get; set; }
    }
}
