using SM64BS.GameMario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace SM64BS.Installers
{
    internal class SM64BSGameInstaller : Installer<SM64BSGameInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameMarioManager>().AsSingle();
        }
    }
}
