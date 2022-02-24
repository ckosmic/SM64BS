namespace SM64BS.Plugins.Interfaces
{
    public interface IEnergyEventHandler : IEventHandler
    {
        void EnergyDidReach0();
        void EnergyDidChange(float energy);
    }
}
