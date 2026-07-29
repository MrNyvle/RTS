using System;
using _Scripts.Unit.Interfaces;
using UnityEngine;
using UnityEngine.Video;

namespace _Scripts.Unit.Commands
{
    public class AttackCommand : IUnitCommand
    {
        enum State
        {
            Guarding,
            MovingInRange,
            Attacking,
            MovingToOrigin
        }

        public string GetState()
        {
            return _state.ToString();
        }

        public bool IsFinished { get; set; }
        public EJobType JobType { set; get; }

        private State _state;
        private Transform target;
        private Health _targetHealth;
        private Vector3 originalPosition;
        private UnitCommandTimer _timer = new UnitCommandTimer();

        public AttackCommand()
        {
            JobType = EJobType.Warrior;
        }

        public void Start(VillageUnit unit)
        {
            _state = State.Guarding;
        }

        public void Tick(VillageUnit unit)
        {
            if (_targetHealth is not null && _targetHealth.IsDead())
                _state = State.MovingToOrigin;
            
            switch (_state)
            {
                case State.Guarding:
                    if (unit.vision.SeesTarget())
                    {
                        originalPosition = unit.transform.position;
                        target = unit.vision.GetClosestTarget();
                        target.TryGetComponent(out _targetHealth);
                        _state = State.MovingInRange;
                    }
                    break;
                
                case State.MovingInRange:
                    unit.job.StartJob(JobType);
                    unit.Movement.MoveTo(target.position, unit.unitStats.GetCombatStat(ECombatStat.RangePoints));
                    if (unit.Movement.Reached())
                        _state = State.Attacking;
                    break;
                
                case State.Attacking:
                    if (IsTooFarFromTarget(unit))
                    {
                        _state = State.MovingInRange;
                        break;
                    }

                    if (_timer.TickTimer(Time.deltaTime))
                    {
                        _targetHealth.Damage(unit.unitStats.GetCombatStat(ECombatStat.AttackPoints));
                        Debug.Log("Attacking | Is Target dead : " + _targetHealth.IsDead());
                        _timer.StartTimer(unit.unitStats.GetCombatStat(ECombatStat.AttackSpeedPoints));
                        if (_targetHealth.IsDead())
                        {
                            _timer.CancelTimer();
                            _state = State.MovingToOrigin;
                        }
                    }
                    break;
                
                case State.MovingToOrigin:
                    if (unit.vision.SeesTarget())
                    {
                        target = unit.vision.GetClosestTarget();
                        target.TryGetComponent(out _targetHealth);
                        _state = State.MovingInRange;
                    }
                    
                    unit.Movement.MoveTo(originalPosition);
                    
                    if (unit.Movement.Reached())
                        unit.job.EndJob();
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public bool IsTooFarFromTarget(VillageUnit unit)
        {
            return Vector3.Distance(unit.transform.position, target.position) > unit.unitStats.GetCombatStat(ECombatStat.RangePoints);
        }

        public float Timer { get; set; }
        
        public void Cancel(VillageUnit unit)
        {
            unit.Movement.Stop();
            _timer.CancelTimer();
            unit.IsAttacking = false;
            IsFinished = true;
        }
    }
}