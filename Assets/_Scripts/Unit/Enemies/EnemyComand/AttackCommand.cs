using _Scripts.Buildings;
using _Scripts.Unit;
using UnityEngine;

namespace _Scripts.Enemies.EnemyComand
{
    public class AttackCommand : IEnemyCommand
    {
        enum State
        {
            MovingToTarget,
            Attacking
        }
        
        State _state;
        Transform _targetTransform;
        Health _targetHealth;
        float _hitDistance;
        UnitMovement _unitMovement;
        
        public bool IsFinished { get; set; }

        public AttackCommand(Transform transform)
        {
            _targetTransform =  transform;
            _targetTransform.TryGetComponent(out _targetHealth);
        }

        public void Start(Enemy unit)
        {
            _state = State.MovingToTarget;
            _unitMovement = unit.Movement;
            _unitMovement.MoveTo(_targetTransform.position);
        }

        public void Tick(Enemy unit)
        {
            switch (_state)
            {
                case State.MovingToTarget:
                    if (_unitMovement.Reached())
                    {
                        _state = State.Attacking;
                    }
                    break;
                case State.Attacking:
                    if (IsTooFarFromTarget(unit))
                    {
                        _state = State.MovingToTarget;
                        break;
                    }
                    _targetHealth.Damage(unit.unitStats.GetCombatStat(ECombatStat.AttackPoints));
                    
                    break;
            }
        }

        public void Cancel(Enemy unit)
        {
            _unitMovement.Stop();
            IsFinished = true;
        }

        public bool IsTooFarFromTarget(Enemy unit)
        {
            return Vector3.Distance(unit.transform.position, _targetTransform.position) > unit.unitStats.GetCombatStat(ECombatStat.RangePoints);
        }
    }
}