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
            Container.Bind<ResourceUtilities>().AsSingle();
            Container.Bind<MarioSpawner>().AsSingle();
        }
    }
}
