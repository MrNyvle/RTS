using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Unit
{
    public class UnitStats
    {
        public Dictionary<EJobType, int> jobsPoint = new Dictionary<EJobType, int>();
        public Dictionary<ECombatStat, float> combatStats = new Dictionary<ECombatStat, float>();
        
        public ECombatArchetype eCombatType;

        public UnitStats(ECombatArchetype eCombatType)
        {
            this.eCombatType = eCombatType;
        }

        public int GetJobPoints(EJobType jobType)
        {
            return jobsPoint.GetValueOrDefault(jobType);
        }
        public float GetCombatStat(ECombatStat combatStat)
        {
            return combatStats.GetValueOrDefault(combatStat);
        }

        public void UpdateJobPoints(EJobType jobType, int points)
        {
            jobsPoint[jobType] = points;

            if (jobType.Equals(EJobType.Warrior))
            {
                RecalculateCombatStats();
            }
        }

        public void RecalculateCombatStats()
        {
            combatStats[ECombatStat.AttackPoints] = GameManager.Instance.GetCombatStats(eCombatType).attackPoints *  (GetJobPoints(EJobType.Warrior)+1f)/10f;
            combatStats[ECombatStat.AttackSpeedPoints] = GameManager.Instance.GetCombatStats(eCombatType).attackSpeedPoints /* *  GetJobPoints(EJobType.Warrior)+1/10*/;
            combatStats[ECombatStat.HealthPoints] = GameManager.Instance.GetCombatStats(eCombatType).healthPoints /* *  (GetJobPoints(EJobType.Warrior)+1f)/10f*/;
            combatStats[ECombatStat.RangePoints] = GameManager.Instance.GetCombatStats(eCombatType).rangePoints /* *  GetJobPoints(EJobType.Warrior)/10 */;
        }

        public int GetTotalPoints()
        {
            int points = 0;
            foreach (var kv in jobsPoint)
            {
                points += kv.Value;
            }
            
            return points;
        }
    }
}