using System;
using _Scripts.Buildings;
using UnityEngine;

namespace _Scripts.Unit.Commands
{
    public class BuildCommand : IUnitCommand
    {
        enum State
        {
            MovingToResourceBuilding,
            GrabbingResource,
            MovingToBuilding,
            DepositingResource,
            Building,
            MovingToBuildingEntrance
        }
        
        State _state;
        Building _building;
        EResource _resourceToFetch;
        int _quantityToFetch;
        float _timer;
        
        public bool IsFinished { get; set; }
        public EJobType JobType { get; set; }

        public BuildCommand(Building building)
        {
            JobType = EJobType.Unemployed;
            _building = building;
        }

        public void Start(VillageUnit unit)
        {
            if (NeedToGetResourcesForBuild())
            {
                _state = State.MovingToResourceBuilding;
                unit.Movement.MoveTo(unit.TownHall.GetBuildingForResource(_resourceToFetch).GetEntrancePosition());
            }
        }

        private bool NeedToGetResourcesForBuild()
        {
            EResource? resource = _building.GetNeededBuildingResource(out _quantityToFetch);
            if ( resource != null)
            {
                _resourceToFetch = (EResource)resource;
            }
            return resource != null;
        }
        
        public void Tick(VillageUnit unit)
        {
            switch (_state)
            {
            case State.MovingToResourceBuilding:
                Debug.Log("Moving to resource building");
                if (unit.Movement.Reached())
                {
                    _state = State.GrabbingResource;
                }
                break;
            case State.GrabbingResource:
                Debug.Log("Grabbing resource");
                bool usedItems = unit.TownHall.GetBuildingForResource(_resourceToFetch).resource.UseResource(_resourceToFetch, _quantityToFetch);
                unit.TownHall.UpdateUI();
                if (usedItems)
                {
                    unit.resource.AddResourceToInv(_resourceToFetch, _quantityToFetch);
                    unit.UpdateUI();
                    unit.Movement.MoveTo(_building.gameObject.transform.position);
                    _state = State.MovingToBuilding;
                    break;
                }
                IsFinished = true;
                break;
            case State.MovingToBuilding:
                Debug.Log("Moving to building");
                if (unit.Movement.Reached())
                {
                    _state = State.DepositingResource;
                }
                break;
            case State.DepositingResource:
                Debug.Log("Depositing Resource");
                _building.DepositBuildingResource(_resourceToFetch, _quantityToFetch);
                if (!unit.resource.RemoveResource(_resourceToFetch, _quantityToFetch))
                {
                    IsFinished = true;
                    break;
                }
                unit.UpdateUI();
                if (NeedToGetResourcesForBuild())
                {
                    unit.Movement.MoveTo(unit.TownHall.GetBuildingForResource(_resourceToFetch).GetEntrancePosition());
                    _state = State.MovingToResourceBuilding;
                    break;
                }
                StartTimer(_building.stats.tiers[_building.currentTier].buildTime);
                _state = State.Building;
                break;
            case State.Building:
                Debug.Log("Building building");
                if (TickTimer(Time.deltaTime))
                {
                    _building.Build();
                    unit.Movement.MoveTo(_building.GetEntrancePosition());
                    _state =  State.MovingToBuildingEntrance;
                }
                break;
            case State.MovingToBuildingEntrance:
                if (unit.Movement.Reached())
                {
                    GameManager.Instance.RebuildNavmesh();
                    IsFinished = true;
                }
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