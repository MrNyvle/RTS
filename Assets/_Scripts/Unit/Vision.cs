using System.Collections.Generic;
using _Scripts.Enemies;
using UnityEngine;

namespace _Scripts.Unit.Commands
{
    public class Vision : MonoBehaviour
    {
        public float viewRadius = 10f;
        public float viewAngle = 90f;
        public LayerMask targetMask;
        public LayerMask obstacleMask;

        public List<Transform> visibleTargets = new();

        public void ListVisibleTargets()
        {
            visibleTargets.Clear();

            Collider[] targets = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
            
            foreach (var col in targets)
            {
                Transform target = col.transform;
                Vector3 dir = (target.position - transform.position).normalized;

                // if (Vector3.Angle(transform.forward, dir) > viewAngle * 0.5f)
                //     continue;

                float dist = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dir, dist, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }

        public bool SeesTarget()
        {
            return visibleTargets.Count > 0;
        }

        public bool SeesHostileTarget()
        {
            return visibleTargets.Count > 0 && visibleTargets[0].TryGetComponent(out VillageUnit villageUnit) && villageUnit.unitStats.eCombatType != ECombatArchetype.Peasant;
        }
        
        public Transform GetClosestHostileTarget()
        {
            if (visibleTargets.Count == 0)
                return null;
            
            Transform target = null;
            float distance = float.MaxValue;
            
            foreach (Transform enemyTransform in visibleTargets)
            {
                enemyTransform.TryGetComponent(out VillageUnit villageUnit);
                if (villageUnit.unitStats.eCombatType == ECombatArchetype.Peasant)
                {
                    nextElement: continue;
                }
                
                float dist = Vector3.Distance(enemyTransform.position , transform.position);

                if (dist < distance)
                {
                    distance = dist;
                    target = enemyTransform;
                }
            }
            return target;
            
        }
        
        public Transform GetClosestTarget()
        {
            if (visibleTargets.Count == 0)
                return null;
            
            Transform target = null;
            float distance = float.MaxValue;
            
            foreach (Transform enemyTransform in visibleTargets)
            {
                float dist = Vector3.Distance(enemyTransform.position , transform.position);

                if (dist < distance)
                {
                    distance = dist;
                    target = enemyTransform;
                }
            }
            return target;
        }

        public Enemy GetClosestTarget2D()
        {
            if (visibleTargets.Count == 0)
                return null;
            
            Transform target = null;
            float distance = float.MaxValue;
            
            foreach (Transform enemyTransform in visibleTargets)
            {
                Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
                Vector2 ePos2D = new Vector2(enemyTransform.position.x, enemyTransform.position.y);
                float dist = Vector2.Distance(ePos2D,  pos2D);

                if (dist < distance)
                {
                    distance = dist;
                    target = enemyTransform;
                }
            }

            Enemy enemy = null;
            if (target != null) target.TryGetComponent(out enemy);
            return enemy;
        }
        
    }
}