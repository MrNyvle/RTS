namespace _Scripts.Unit.Commands
{
    public interface IUnitCommandRepeatable : IUnitCommand
    {
        void Repeat(VillageUnit unit)
        {
        }
    }
}