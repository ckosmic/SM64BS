using SM64BS.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace SM64BS.Installers
{
    internal class SM64BSAppInstaller : Installer<SM64BSAppInstaller>
    {
        public override void InstallBindings()
        {
            ResourceUtilities.mainBundleResourcePath = $"SM64BS.Resources.assets.unity3d";
            Container.BindInterfacesAndSelfTo<ResourceUtilities>().AsSingle();
            Container.Bind<MarioSpawner>().AsSingle();
        }
    }
}
