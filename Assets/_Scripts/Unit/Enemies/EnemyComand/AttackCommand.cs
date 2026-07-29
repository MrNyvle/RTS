using _Scripts.Buildings;
using _Scripts.Unit;
using _Scripts.Unit.Interfaces;
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
        private UnitCommandTimer _timer;
        private Vector3 destination;

        public bool IsFinished { get; set; }

        public AttackCommand(Transform transform)
        {
            _targetTransform = transform;
            _targetTransform.TryGetComponent(out _targetHealth);
        }

        public void Start(Enemy unit)
        {
            _state = State.MovingToTarget;
        }

        public void Tick(Enemy unit)
        {
            switch (_state)
            {
                case State.MovingToTarget:
                    Debug.Log("Moving to target");
                    if (destination != _targetTransform.position)
                    {
                        unit.Movement.MoveTo(_targetTransform.position, unit.unitStats.GetCombatStat(ECombatStat.RangePoints));
                        destination = _targetTransform.position;
                    }
                    
                    if (unit.Movement.Reached())
                    {
                        unit.Movement.Stop();
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
            unit.Movement.Stop();
            IsFinished = true;
        }

        public bool IsTooFarFromTarget(Enemy unit)
        {
            return Vector3.Distance(unit.transform.position, _targetTransform.position) >=
                   unit.unitStats.GetCombatStat(ECombatStat.RangePoints);
        }
    }
}