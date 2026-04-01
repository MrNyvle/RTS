using System;
using _Scripts.Buildings;
using _Scripts.UI.Villager;
using _Scripts.Unit.Commands;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace _Scripts.Unit
{
    [RequireComponent(typeof(NavMeshAgent), typeof(UnitMovement), typeof(Vision))]
    [RequireComponent(typeof(Health))]
    public class VillageUnit : MonoBehaviour
    {
        IUnitCommand _currentCommand;
        Health _health;
        TownHall _townHall;
        UnitMovement _unitMovement;
        
        private Renderer _renderer;
        private MaterialPropertyBlock _block;
        private static readonly int SelectedID = Shader.PropertyToID("_Selected");
        
        public bool IsAttacking { get; set; }
        public bool IsFleeing { get; set; }

        public UnitResource resource = new ();
        public VillagerUI villagerUI;
        public UnitVillagerJob job;
        public Vision vision;
        public UnitStats unitStats ;
        public TownHall TownHall => _townHall;
        public UnitMovement Movement => _unitMovement;

        public int lumberjackPoint;
        [ReadOnly] public String commandName; 
        [ReadOnly] public String state; 
        
        private void Awake()
        {
            unitStats = new UnitStats(ECombatArchetype.Peasant);
            job = new UnitVillagerJob(unitStats);
            TryGetComponent(out vision);
            TryGetComponent(out _unitMovement);
            TryGetComponent(out _health);
            
            _renderer = GetComponent<Renderer>();
            _block = new MaterialPropertyBlock();
        }
        
        private void Start()
        {
            UpdateUI();
        }

        [Button]
        public void UpdateUI()
        {
            villagerUI.UpdateUI(resource);
        }
        
        public void SetSelected(bool selected)
        {
            _renderer.GetPropertyBlock(_block);
            _block.SetFloat(SelectedID, selected ? 1f : 0f);
            _renderer.SetPropertyBlock(_block);
        }

        public void AssignCombatType(ECombatArchetype combatType)
        {
            unitStats.eCombatType = combatType;
            unitStats.RecalculateCombatStats();
        }
        
        public void AssignTownHall(TownHall tHall)
        {
            _townHall = tHall;
        }

        void Update()
        {
            commandName = _currentCommand?.GetType().Name;
            state = _currentCommand?.GetState();
            
            lumberjackPoint = unitStats.GetJobPoints(EJobType.Lumberjack);

            vision.ListVisibleTargets();
            
            if (vision.SeesTarget())
            {
                if (unitStats.eCombatType == ECombatArchetype.Peasant && !IsFleeing)
                {
                    IsFleeing = true;
                    IssueCommand(new Flee(vision));
                }
                else if (!IsAttacking)
                {
                    IsAttacking = true;
                    IssueCommand(new AttackComand());
                }
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
            resource.AddResourceToInv(eResource, quantity);
            villagerUI.UpdateUI(resource);
        }

        public void DepositResources(BuildingResource resource)
        {
            this.resource.DepositResourceToBuilding(resource);
            villagerUI.UpdateUI(this.resource);
            _townHall.UpdateUI();
        }
        
        public void ClearResources()
        {
            resource.Clear();
            villagerUI.UpdateUI(resource);
        }
    }
}