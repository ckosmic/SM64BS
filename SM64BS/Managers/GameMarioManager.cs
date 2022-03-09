using Zenject;
using LibSM64;

namespace SM64BS.Managers
{
    internal class GameMarioManager : MarioSceneManager, ILateDisposable
    {
        private readonly AppMarioManager _appMarioManager;

        public GameMarioManager(AppMarioManager appMarioManager, ScoreController scoreController, GameEnergyCounter gameEnergyCounter)
{
            _appMarioManager = appMarioManager;
        }

        public override void Initialize()
        {
            base.Initialize();

            _appMarioManager.CreateMenuBufferPlatform();
            _appMarioManager.SetMenuTerrainsEnabled(false);
        }

        public void LateDispose()
        {
            _appMarioManager.SetMenuTerrainsEnabled(true);
            SM64Context.SetScaleFactor(2.0f);
        }
    }
}
