using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Unit.Commands
{
    public class Flee : IUnitCommand
    {
        private Vision _vision;
        private VillageUnit _unit;

        private float _fleeDistance = 10f;
        private float _safeDistance = 15f;
        private float _updateInterval = 0.2f;
        private float _timer;

        private bool _finished;
        public bool IsFinished => _finished;
        public EJobType JobType { get; set; } = EJobType.Unemployed;

        public Flee(Vision vision)
        {
            _vision = vision;
        }

        public void Start(VillageUnit unit)
        {
            _unit = unit;
            _finished = false;
            _timer = 0f;
        }

        public void Tick(VillageUnit unit)
        {
            if (_vision.visibleTargets.Count == 0)
            {
                // No enemies, stop fleeing
                _finished = true;
                unit.Movement.Stop();
                return;
            }

            _timer -= Time.deltaTime;
            if (_timer > 0f) return; // update at intervals
            _timer = _updateInterval;

            // Compute flee direction
            Vector3 enemyCenter = GetAverageEnemyPosition();
            Vector3 fleeDir = (_unit.transform.position - enemyCenter).normalized;
            Vector3 fleeTarget = _unit.transform.position + fleeDir * _fleeDistance;

            // Move the unit
            unit.Movement.MoveTo(fleeTarget);

            // Check if safe
            if (Vector3.Distance(_unit.transform.position, enemyCenter) > _safeDistance)
            {
                _finished = true;
                unit.Movement.Stop();
            }
        }

        public void Cancel(VillageUnit unit)
        {
            _finished = true;
            unit.Movement.Stop();
        }

        private Vector3 GetAverageEnemyPosition()
        {
            var list = _vision.visibleTargets;
            if (list.Count == 0) return _unit.transform.position;

            Vector3 sum = Vector3.zero;
            foreach (var e in list) sum += e.position;
            return sum / list.Count;
        }
    }
}
