namespace _Scripts.Unit.Commands
{
    public interface IUnitCommand
    {
        void Start(VillageUnit unit);
        void Tick(VillageUnit unit);
        void Cancel(VillageUnit unit);
        bool IsFinished { get; }
    }
}