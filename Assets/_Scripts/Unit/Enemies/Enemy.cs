using System.Linq;
using _Scripts.Buildings;
using _Scripts.Enemies.EnemyComand;
using _Scripts.Unit;
using _Scripts.Unit.Commands;
using UnityEngine;
using UnityEngine.AI;
using AttackCommand = _Scripts.Enemies.EnemyComand.AttackCommand;

namespace _Scripts.Enemies
{
    [RequireComponent(typeof(NavMeshAgent), typeof(Vision),  typeof(UnitMovement))]
    [RequireComponent(typeof(Health))]
    public class Enemy : MonoBehaviour
    {
        public TownHall townHall;
        Vision _vision;
        UnitMovement _unitMovement;
        Health _unitHealth;
        IEnemyCommand _currentCommand;
        ECombatArchetype _combatArchetype;
        public UnitStats unitStats = new UnitStats(ECombatArchetype.Basic);
    
        public UnitMovement Movement => _unitMovement;
        
        Transform _target;
    
        private void Start()
        {
            TryGetComponent(out _vision);
            TryGetComponent(out _unitMovement);
            TryGetComponent(out _unitHealth);

            _unitHealth.onDeath.AddListener(() => EnemyWaveSystem.Instance.OnUnitDeath(this));
            
            townHall = GameManager.Instance.townHalls.First();
            
            unitStats.RecalculateCombatStats();
            
            _target = townHall.transform;
            
            IssueCommand(new AttackCommand(_target));
        }

        void Update()
        {
            _vision.ListVisibleTargets();
            
            Transform target = _vision.GetClosestHostileTarget() ?? _vision.GetClosestTarget();
            
            if (target is not null && target != _target)
            {
                _target = target;
                IssueCommand(new AttackCommand(_target));
            }
            
            if (_currentCommand != null && _currentCommand.IsFinished)
            {
                IssueCommand(new AttackCommand(townHall.transform));
            }
            
            _currentCommand?.Tick(this);
        }

        public void IssueCommand(IEnemyCommand command)
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
    }
}
