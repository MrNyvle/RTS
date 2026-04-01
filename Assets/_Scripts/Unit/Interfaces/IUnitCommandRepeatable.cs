namespace _Scripts.Unit.Commands
{
    public interface IUnitCommandRepeatable : IUnitCommand
    {
        bool IUnitCommand.IsRepeatable => true;
        void Repeat(VillageUnit unit);
    }
}