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
        float _timer;
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
                    if (move.Reached())
                    {
                        StartTaking();
                        _state = State.Harvesting;
                    }
                    break;
    
                case State.Harvesting:
                    if (TickTaking(Time.deltaTime))
                    {
                        _resource.FinishTaking(unit);
                        move.MoveTo(unit.townHall.GetEntrancePosition());
                        _state = State.MovingToTownHall;
                    }
                    break;
    
                case State.MovingToTownHall:
                    if (move.Reached())
                    {
                        _state = State.Depositing;
                    }
                    break;
    
                case State.Depositing:
                    unit.townHall.Deposit(unit);
                    _finished = true;
                    break;
            }
        }
        
        public void StartTaking()
        {
            _timer = _resource.harvestTime;
        }

        public bool TickTaking(float deltaTime)
        {
            if (!_resource.isValid) return true;
            
            _timer -= deltaTime;
            
            return _timer <= 0f;
        }

        public void Cancel(VillageUnit unit)
        {
            unit.Movement.Stop();
            CancelTaking();
            _finished = true;
        }

        public void CancelTaking()
        {
            _timer = 0f;
        }

        public void Repeat(VillageUnit unit)
        {
            if (_resource.eResourceType == EResourceType.Unlimited)
            {
                _finished = false;
                Start(unit);
            }
        }
    }
}