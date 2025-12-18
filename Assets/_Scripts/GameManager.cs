using System;
using System.Collections.Generic;
using _ScriptableObjects.VillageUnit;
using _ScriptableObjects.VillageUnit.JobScaling;
using _Scripts.Buildings;
using _Scripts.Unit;
using UnityEngine;

namespace _Scripts
{
    public class GameManager : Singleton<GameManager>
    {
        public VillageUnit SelectedUnit { get; set; }
        public List<VillageUnitCombatStats> villageUnitCombatStats;
        public JobScalingBalancer  jobScalingBalancers;
        public List<TownHall> townHalls;
        
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
            {EJobType.Miner, EResource.Stone},
            {EJobType.Farmer, EResource.Food},
            {EJobType.Seller, EResource.Gold},
            {EJobType.Priest, EResource.Shiny}
        };
        public Dictionary<EResource, EBuilding> resourceToBuilding =  new Dictionary<EResource, EBuilding>()
        {
            {EResource.Wood, EBuilding.TimberLodger},
            {EResource.Stone, EBuilding.PebbleTemple},
            {EResource.Food, EBuilding.BreadShed},
            {EResource.Gold, EBuilding.CashStash},
            {EResource.Shiny, EBuilding.ShineShrine}
        };
        public Dictionary<EBuilding, EResource> buildingToResource =  new Dictionary<EBuilding, EResource>()
        {
            {EBuilding.TimberLodger, EResource.Wood},
            {EBuilding.PebbleTemple, EResource.Stone},
            {EBuilding.BreadShed, EResource.Food},
            {EBuilding.CashStash, EResource.Gold},
            {EBuilding.ShineShrine, EResource.Shiny}
        };
        public Dictionary<EBuilding, EJobType> buildingToJob =  new Dictionary<EBuilding, EJobType>()
        {
            {EBuilding.TimberLodger, EJobType.Lumberjack},
            {EBuilding.PebbleTemple, EJobType.Miner},
            {EBuilding.BreadShed, EJobType.Farmer},
            {EBuilding.CashStash, EJobType.Seller},
            {EBuilding.ShineShrine, EJobType.Priest}
        };
        public Dictionary<EJobType, EBuilding> jobToBuilding =  new Dictionary<EJobType, EBuilding>()
        {
            {EJobType.Lumberjack, EBuilding.TimberLodger},
            { EJobType.Miner,  EBuilding.PebbleTemple},
            {EJobType.Farmer, EBuilding.BreadShed},
            {EJobType.Seller, EBuilding.CashStash},
            { EJobType.Priest, EBuilding.ShineShrine}
        };
        
        public Camera mainCamera;
        

        public VillageUnitCombatStats GetCombatStats(ECombatArchetype eCombatType)
        {
            return villageUnitCombatStats.Find(stats => stats.eCombatType == eCombatType);
        }
    }
}