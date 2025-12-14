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
        
        
        Dictionary<EJobType, int> timeWorkedAtJob = new ();
        
        ECombatType _eCombatType;
        
        public UnitCombatStats GetCombatStats()
        {
            return new UnitCombatStats(_eCombatType, warriorPoints);
        }

        public void LevelUpJob(EJobType eJobType)
        {
            switch (eJobType)
            {
                case EJobType.Warrior:
                    warriorPoints += 1;
                    break;
                case EJobType.Priest:
                    preistPoints += 1;
                    break;
                case EJobType.Lumberjack:
                    lumberjackPoints += 1;
                    break;
                case EJobType.Miner:
                    minerPoints += 1;
                    break;
                case EJobType.Worker:
                    workerPoints += 1;
                    break;
            }
        }
        
    }
}