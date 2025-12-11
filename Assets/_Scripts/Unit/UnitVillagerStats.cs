using System;
using System.Collections.Generic;

namespace _Scripts.Unit
{
    public class UnitVillagerStats
    {
        private int warriorPoints = 0;
        private int preistPoints = 0;
        private int workerPoints = 0;
        private int lumberjackPoints = 0;
        private int minerPoints = 0;
        
        
        Dictionary<JobType, int> timeWorkedAtJob = new ();
        
        CombatType combatType;
        
        public UnitCombatStats GetCombatStats()
        {
            return new UnitCombatStats(combatType, warriorPoints);
        }

        public void LevelUpJob(JobType jobType)
        {
            switch (jobType)
            {
                case JobType.Warrior:
                    warriorPoints += 1;
                    break;
                case JobType.Priest:
                    preistPoints += 1;
                    break;
                case JobType.Lumberjack:
                    lumberjackPoints += 1;
                    break;
                case JobType.Miner:
                    minerPoints += 1;
                    break;
                case JobType.Worker:
                    workerPoints += 1;
                    break;
            }
        }
        
    }
}