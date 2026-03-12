using _Scripts.Buildings;
using NUnit.Framework;
using UnityEngine;

namespace _Scripts.Unit.Commands
{
    public class DepositCommand : IUnitCommand
    {
        enum State
        {
            MovingToTownHall,
            StartDepositing,
            Depositing,
        }

        private Building _building;
        private State _state;
        float _timer;

        public string GetState()
        {
            return _state.ToString();
        }

        public bool IsFinished { get; set; }
        public EJobType JobType { get; set; }

        public DepositCommand(Building building)
        {
            _building = building;
            JobType = EJobType.Unemployed;
            IsFinished = false;
        }

        public void Start(VillageUnit unit)
        {
            JobType = GameManager.Instance.resourceToJob[unit.resource.GetMostResource(out _)];
            unit.job.StartJob(JobType);
            unit.Movement.MoveTo(_building.GetEntrancePosition());
        }

        public void Tick(VillageUnit unit)
        {
            UnitMovement move = unit.Movement;
            
            switch (_state)
            {
                case State.MovingToTownHall:
                    Debug.Log("Moving To Town Hall");
                    if (move.Reached())
                    {
                        StartTimer(_building.depositTime);
                        _state = State.Depositing;
                    }
                    break;

                case State.StartDepositing:
                    if (TickTimer(Time.deltaTime))
                    {
                        _state = State.Depositing;
                    }
                    break;
                
                case State.Depositing:
                    Debug.Log("Depositing");
                    unit.DepositResources(_building.resource);
                    unit.job.EndJob();
                    IsFinished = true;
                    break;
            }
        }

        public void StartTimer(float time)
        {
            _timer = time;
        }

        public bool TickTimer(float deltaTime)
        {
            _timer -= deltaTime;
            
            return _timer <= 0f;
        }
        
        public void CancelTimer()
        {
            _timer = 0f;
        }
        
        public void Cancel(VillageUnit unit)
        {
            unit.Movement.Stop();
            CancelTimer();
            IsFinished = true;
        }
    }
}