namespace _Scripts.Enemies.EnemyComand
{
    public interface IEnemyCommand
    {
        void Start(Enemy unit);
        void Tick(Enemy unit);
        void Cancel(Enemy unit);
        bool IsFinished { get; }
        bool IsRepeatable => false;
    
    }
}