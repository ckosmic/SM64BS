using SM64BS.Plugins.Interfaces;

namespace SM64BS.EventBroadcasters
{
    internal class EnergyEventBroadcaster : EventBroadcaster<IEnergyEventHandler>
    {
        private GameEnergyCounter _gameEnergyCounter;

        public EnergyEventBroadcaster(GameEnergyCounter gameEnergyCounter)
        { 
            _gameEnergyCounter = gameEnergyCounter;
        }

        public override void Initialize()
        {
            _gameEnergyCounter.gameEnergyDidReach0Event += EnergyDidReach0Handler;
            _gameEnergyCounter.gameEnergyDidChangeEvent += EnergyDidChangeHandler;
        }

        public override void Dispose()
        {
            _gameEnergyCounter.gameEnergyDidReach0Event -= EnergyDidReach0Handler;
            _gameEnergyCounter.gameEnergyDidChangeEvent -= EnergyDidChangeHandler;
        }

        private void EnergyDidReach0Handler()
        {
            foreach (IEnergyEventHandler eventHandler in EventHandlers)
            {
                eventHandler?.EnergyDidReach0();
            }
        }

        private void EnergyDidChangeHandler(float energy)
        {
            foreach (IEnergyEventHandler eventHandler in EventHandlers)
            {
                eventHandler?.EnergyDidChange(energy);
            }
        }
    }
}
