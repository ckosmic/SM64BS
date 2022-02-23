using IPA;
using IPA.Config;
using IPA.Config.Stores;
using LibSM64;
using SiraUtil.Zenject;
using SM64BS.Installers;
using SM64BS.Plugins;
using SM64BS.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using IPALogger = IPA.Logging.Logger;

namespace SM64BS
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        internal static SettingsStore Settings { get; private set; }
        internal static IPALogger Log { get; private set; }
        internal static Dictionary<Tuple<Assembly, int>, CustomPlugin> LoadedCustomPlugins { get; private set; } = new Dictionary<Tuple<Assembly, int>, CustomPlugin>();

        [Init]
        public Plugin(IPALogger logger, Config config, Zenjector zenject)
        {
            Settings = config.Generated<SettingsStore>();
            Log = logger;

            zenject.Install<SM64BSAppInstaller>(Location.App);
            zenject.Install<SM64BSMenuInstaller>(Location.Menu);
            zenject.Install<SM64BSGameInstaller>(Location.StandardPlayer);
        }
    }
}
