using System;
using System.Collections.Generic;

namespace _Scripts.Unit
{
    public class UnitVillagerStats
    {
        public Dictionary<EJobType, int> jobsPoint = new Dictionary<EJobType, int>();
        
        ECombatArchetype _eCombatType;
        
        public UnitCombatStats GetCombatStats()
        {
            return new UnitCombatStats(_eCombatType, GetJobPoints(EJobType.Warrior));
        }

        public int GetJobPoints(EJobType jobType)
        {
            return jobsPoint.GetValueOrDefault(jobType);
        }

        public void UpdateJobPoints(EJobType jobType, int points)
        {
            jobsPoint[jobType] = points;
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