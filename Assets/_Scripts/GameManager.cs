using System.Collections.Generic;
using _ScriptableObjects.VillageUnit;
using _ScriptableObjects.VillageUnit.JobScaling;
using _Scripts.Unit;
using UnityEngine;

namespace _Scripts
{
    public class GameManager : Singleton<GameManager>
    {
        public VillageUnit SelectedUnit { get; set; }
        public List<VillageUnitCombatStats> villageUnitCombatStats;
        public JobScalingBalancer  jobScalingBalancers;
        
        public Dictionary<EResource, EJobType> resourceToJob =  new Dictionary<EResource, EJobType>()
        {
            {EResource.Wood, EJobType.Lumberjack},
            {EResource.Stone, EJobType.Miner},
            {EResource.Food, EJobType.Farmer},
            {EResource.Gold, EJobType.Seller},
            {EResource.Shiny, EJobType.Priest}
        };
        public Dictionary<EJobType, EResource> jobToResource =  new Dictionary<EJobType, EResource>()
        {
            {EJobType.Lumberjack, EResource.Wood},
            { EJobType.Miner, EResource.Stone},
            {EJobType.Farmer, EResource.Food},
            {EJobType.Seller, EResource.Gold},
            { EJobType.Priest, EResource.Shiny}
        };
        
        public Camera mainCamera;
        

        public VillageUnitCombatStats GetCombatStats(ECombatArchetype eCombatType)
        {
            return villageUnitCombatStats.Find(stats => stats.eCombatType == eCombatType);
        }
    }
}