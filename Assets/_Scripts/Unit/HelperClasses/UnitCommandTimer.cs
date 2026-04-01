namespace _Scripts.Unit.Interfaces
{
    public class UnitCommandTimer
    {
        public UnitCommandTimer(float timer = 0)
        {
            Timer = timer;
        }

        private float Timer { get; set; }

        public void StartTimer(float time)
        {
            Timer = time;
        }

        public bool TickTimer(float deltaTime)
        {
            Timer -= deltaTime;
            
            return Timer <= 0f;
        }

        public void CancelTimer()
        {
            Timer = 0f;
        }
    }
}