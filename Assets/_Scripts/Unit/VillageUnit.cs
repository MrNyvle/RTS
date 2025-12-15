using System;
using _Scripts.Buildings;
using _Scripts.Resource;
using _Scripts.UI.Villager;
using _Scripts.Unit.Commands;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.Unit
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class VillageUnit : MonoBehaviour
    {
        public UnitResource unitResource = new UnitResource();
        public TownHall townHall;
        public VillagerUI villagerUI;
        
        IUnitCommand _currentCommand;
        UnitMovement _unitMovement;
        UnitVillagerStats _unitVillagerStats = new UnitVillagerStats();

        public UnitMovement Movement => _unitMovement;
        
        private void Awake()
        {
            TryGetComponent(out _unitMovement);
        }

        [Button]
        private void Start()
        {
            villagerUI.UpdateUI(unitResource);
        }

        public void AssignTownHall(TownHall tHall)
        {
            townHall = tHall;
        }

        void Update()
        {
            _currentCommand?.Tick(this);

            if (_currentCommand != null && _currentCommand.IsFinished)
            {
                if (_currentCommand.IsRepeatable)
                {
                    ((IUnitCommandRepeatable)_currentCommand).Repeat(this);
                }
                else
                {
                    _currentCommand = null;
                }
            }
        }

        public void IssueCommand(IUnitCommand command)
        {
            _currentCommand?.Cancel(this);
            _currentCommand = command;
            _currentCommand.Start(this);
        }

        public void StopAllActions()
        {
            _currentCommand?.Cancel(this);
            _currentCommand = null;
        }

        public void AddResource(EResource eResource, int quantity)
        {
            unitResource.AddResourceToInv(eResource, quantity);
            villagerUI.UpdateUI(unitResource);
        }

        public void ClearResources()
        {
            unitResource.Clear();
            villagerUI.UpdateUI(unitResource);
        }
    }
}