using SM64BS.Managers;
using SM64BS.Plugins;
using SM64BS.Utils;
using Zenject;

namespace SM64BS.Installers
{
    internal class SM64BSAppInstaller : Installer<SM64BSAppInstaller>
    {
        public override void InstallBindings()
        {
            ResourceUtilities.MainBundleResourcePath = $"SM64BS.Resources.assets.unity3d";
            ResourceUtilities.MainBundleAssetsPath = @"Assets/SM64BS/";
            Container.BindInterfacesAndSelfTo<BuiltInPluginLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<ResourceUtilities>().AsSingle();
            Container.BindInterfacesAndSelfTo<AppMarioManager>().AsSingle();
        }
    }
}
