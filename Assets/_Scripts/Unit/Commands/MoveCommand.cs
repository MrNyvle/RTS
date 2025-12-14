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
        bool _finished;
        
        public bool IsFinished => _finished;

        public MoveCommand(Vector3 movePosition)
        {
            _movePosition = movePosition;
            _finished = false;
        }
        
        public void Start(VillageUnit unit)
        {
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
                        _finished = true;
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