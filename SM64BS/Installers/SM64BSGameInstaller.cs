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
