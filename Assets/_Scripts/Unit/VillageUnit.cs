using System;
using _Scripts.Buildings;
using _Scripts.UI.Villager;
using _Scripts.Unit.Commands;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace _Scripts.Unit
{
    [RequireComponent(typeof(NavMeshAgent), typeof(UnitMovement), typeof(Vision))]
    [RequireComponent(typeof(Health))]
    public class VillageUnit : MonoBehaviour
    {
        public UnitResource unitResource = new ();
        public VillagerUI villagerUI;
        public UnitVillagerJob unitVillagerJob;

        public int lumberjackPoint;

        IUnitCommand _currentCommand;
        Health _health;
        Vision _vision;
        TownHall _townHall;
        
        UnitMovement _unitMovement;
        public UnitStats unitStats = new UnitStats();

        public TownHall TownHall => _townHall;
        public UnitMovement Movement => _unitMovement;
        
        private void Awake()
        {
            unitVillagerJob = new UnitVillagerJob(unitStats);
            TryGetComponent(out _vision);
            TryGetComponent(out _unitMovement);
            TryGetComponent(out _health);
        }
        
        private void Start()
        {
            UpdateUI();
        }

        [Button]
        public void UpdateUI()
        {
            villagerUI.UpdateUI(unitResource);
        }

        public void AssignCombatType(ECombatArchetype combatType)
        {
            unitStats.eCombatType = combatType;
        }
        
        public void AssignTownHall(TownHall tHall)
        {
            _townHall = tHall;
        }

        void Update()
        {
            lumberjackPoint = unitStats.GetJobPoints(EJobType.Lumberjack);

            _vision.ListVisibleTargets();
            
            if (_vision.SeesTarget())
            {
                Debug.Log("Enemy Seen");
                IssueCommand(new Flee(_vision));
            }
            
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

        public void DepositResources(BuildingResource resource)
        {
            unitResource.DepositResourceToBuilding(resource);
            villagerUI.UpdateUI(unitResource);
            _townHall.UpdateUI();
        }
        
        public void ClearResources()
        {
            unitResource.Clear();
            villagerUI.UpdateUI(unitResource);
        }
    }
}