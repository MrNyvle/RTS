using _Scripts.Buildings;
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
            MovingToBuilding,
            StartDepositing,
            Depositing
        }
    
        State _state;
        GameResource _resource;
        Building _building;
        float _timer;
    
        public bool IsFinished { get; set; }
        public float Timer { get; set; }
        public EJobType JobType { get; set; }

        public GatherResourceCommand(GameResource resource)
        {
            JobType = EJobType.Lumberjack;
            _resource = resource;
            IsFinished = false;
        }
    
        public void Start(VillageUnit unit)
        {
            unit.job.StartJob(JobType);
            _state = State.MovingToResource;
            unit.Movement.MoveTo(_resource.GetPosition());
            _building = unit.TownHall.GetBuildingForResource(_resource.eResource);
        }
    
        public void Tick(VillageUnit unit)
        {
            var move = unit.Movement;
    
            switch (_state)
            {
                case State.MovingToResource:
                    if (move.Reached())
                    {
                        StartTimer(_resource.harvestTime);
                        _state = State.Harvesting;
                    }
                    break;
    
                case State.Harvesting:
                    if (!_resource.isValid)
                    {
                        Cancel(unit);
                        break;
                    } 
                    if (TickTimer(Time.deltaTime))
                    {
                        _resource.FinishTaking(unit);
                        move.MoveTo(_building.GetEntrancePosition());
                        _state = State.MovingToBuilding;
                    }
                    break;
    
                case State.MovingToBuilding:
                    if (move.Reached())
                    {
                        StartTimer(_building.depositTime);
                        _state = State.StartDepositing;
                    }
                    break;
                
                case State.StartDepositing:
                    if (TickTimer(Time.deltaTime))
                    {
                        _state = State.Depositing;
                    }
                    break;
                
                case State.Depositing:
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
            if (!_resource.isValid) return true;
            
            _timer -= deltaTime;
            
            return _timer <= 0f;
        }

        public void Cancel(VillageUnit unit)
        {
            unit.Movement.Stop();
            CancelTimer();
            IsFinished = true;
        }

        public void CancelTimer()
        {
            _timer = 0f;
        }

        public void Repeat(VillageUnit unit)
        {
            if (_resource.eResourceType == EResourceType.Unlimited)
            {
                IsFinished = false;
                Start(unit);
            }
        }
    }
}