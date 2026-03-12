using System;
using UnityEngine;

namespace _Scripts.Unit.Commands
{
    public class MoveCommand : IUnitCommand
    {
        enum State
        {
            MovingToPosition,
        }

        State _state;
        Vector3 _movePosition;

        public string GetState()
        {
            return _state.ToString();
        }

        public bool IsFinished { get; set; }
        public EJobType JobType { get; set; }

        public MoveCommand(Vector3 movePosition)
        {
            JobType = EJobType.Unemployed;
            _movePosition = movePosition;
            IsFinished = false;
        }

        public void Start(VillageUnit unit)
        {
            unit.job.StartJob(JobType);
            unit.Movement.MoveTo(_movePosition);
            _state = State.MovingToPosition;
        }

        public void Tick(VillageUnit unit)
        {
            switch (_state)
            {
                case State.MovingToPosition:
                    if (unit.Movement.Reached())
                    {
                        IsFinished = true;
                        unit.job.EndJob();
                    }
                    break;
            }
        }

        public void Cancel(VillageUnit unit)
        {
            unit.Movement.Stop();
        }
    }
}