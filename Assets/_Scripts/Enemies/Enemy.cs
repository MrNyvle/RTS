using System.Linq;
using _Scripts.Buildings;
using _Scripts.Enemies.EnemyComand;
using _Scripts.Unit;
using _Scripts.Unit.Commands;
using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.Enemies
{
    [RequireComponent(typeof(NavMeshAgent), typeof(Vision),  typeof(UnitMovement))]
    [RequireComponent(typeof(Health))]
    public class Enemy : MonoBehaviour
    {
        public TownHall townHall;
        Vision _vision;
        UnitMovement _unitMovement;
        IEnemyCommand _currentCommand;
        ECombatArchetype _combatArchetype;
        public UnitStats unitStats = new UnitStats();
    
        public UnitMovement Movement => _unitMovement;
    
        private void Start()
        {
            TryGetComponent(out _vision);
            TryGetComponent(out _unitMovement);

            townHall = GameManager.Instance.townHalls.First();
            
            unitStats.RecalculateCombatStats();
            
            IssueCommand(new AttackCommand(townHall.transform));
        }

        void Update()
        {
            _vision.ListVisibleTargets();
            
            if (_vision.SeesTarget())
            {
                Debug.Log("Unit Seen");
                IssueCommand(new AttackCommand(_vision.GetClosestTarget().transform));
            }
            
            _currentCommand?.Tick(this);
            
            if (_currentCommand != null && _currentCommand.IsFinished)
            {
                IssueCommand(new AttackCommand(townHall.transform));
            }
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
