using _Scripts.Buildings;
using UnityEngine;

namespace _Scripts.Unit.Commands
{
    public class DepositCommand : IUnitCommand
    {
        enum State
        {
            MovingToTownHall,
            Depositing,
        }

        private State _state;
        bool _finished;

        public bool IsFinished => _finished;
        public EJobType JobType { get; set; }

        public DepositCommand()
        {
            JobType = EJobType.Unemployed;
            _finished = false;
        }

        public void Start(VillageUnit unit)
        {
            JobType = GameManager.Instance.resourceToJob[unit.unitResource.GetMostResource(out _)];
            unit.unitVillagerJob.StartJob(JobType);
            unit.Movement.MoveTo(unit.townHall.GetEntrancePosition());
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
                        _state = State.Depositing;
                    }
                    break;

                case State.Depositing:
                    Debug.Log("Depositing");
                    unit.townHall.Deposit(unit);
                    unit.unitVillagerJob.EndJob();
                    _finished = true;
                    break;
            }
        }

        public void Cancel(VillageUnit unit)
        {
            unit.Movement.Stop();
            _finished = true;
        }
    }
}