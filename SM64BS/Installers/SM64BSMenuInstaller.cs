using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace SM64BS.Installers
{
    internal class SM64BSMenuInstaller : Installer<SM64BSMenuInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<MenuMarioManager>().AsSingle();
        }
    }
}
