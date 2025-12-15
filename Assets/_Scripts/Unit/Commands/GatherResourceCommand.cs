using System;
using _Scripts.Resource;
using UnityEngine;

namespace _Scripts.Unit.Commands
{
    public class GatherResourceCommand : IUnitCommandRepeatable
    {
         enum State
        {
            MovingToResource,
            Harvesting,
            MovingToTownHall,
            Depositing
        }
    
        State _state;
        GameResource _resource;
        bool _finished;
    
        public bool IsFinished => _finished;
        
        public GatherResourceCommand(GameResource resource)
        {
            _resource = resource;
            _finished = false;
        }
    
        public void Start(VillageUnit unit)
        {
            unit.Movement.MoveTo(_resource.GetPosition());
            _state = State.MovingToResource;
        }
    
        public void Tick(VillageUnit unit)
        {
            var move = unit.Movement;
    
            switch (_state)
            {
                case State.MovingToResource:
                    Debug.Log("Moving To Resource");
                    if (move.Reached())
                    {
                        _resource.StartTaking();
                        _state = State.Harvesting;
                    }
                    break;
    
                case State.Harvesting:
                    Debug.Log("Harvesting");
                    
                    if (_resource.TickTaking(Time.deltaTime))
                    {
                        _resource.FinishTaking(unit);
                        move.MoveTo(unit.townHall.GetEntrancePosition());
                        _state = State.MovingToTownHall;
                    }
                    break;
    
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
                    _finished = true;
                    break;
            }
        }
    
        public void Cancel(VillageUnit unit)
        {
            unit.Movement.Stop();
            _resource?.CancelTaking();
            _finished = true;
        }
    
        public void Repeat(VillageUnit unit)
        {
            _finished =  false;
            Start(unit);
        }
    }
}