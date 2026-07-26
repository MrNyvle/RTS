using System;
using System.Collections.Generic;
using _ScriptableObjects.VillageUnit;
using _ScriptableObjects.VillageUnit.JobScaling;
using _Scripts.Buildings;
using _Scripts.Enemies;
using _Scripts.Unit;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace _Scripts
{
    public class GameManager : Singleton<GameManager>
    {
        public VillageUnit SelectedUnit { get; set; }
        public Building SelectedBuilding { get; set; }
        public NavMeshSurface navMesh;
        public List<VillageUnitCombatStats> villageUnitCombatStats;
        public VillageUnitPrice villageUnitPrice;
        public List<ArchetypePrefab> archetypePrefabs;
        public JobScalingBalancer  jobScalingBalancers;
        public List<TownHall> townHalls;
        public TimeSpan GameTime;
        
        private DateTime start;
        
        public readonly Dictionary<EResource, EJobType> resourceToJob =  new Dictionary<EResource, EJobType>()
        {
            {EResource.Wood, EJobType.Lumberjack},
            {EResource.Stone, EJobType.Miner},
            {EResource.Food, EJobType.Farmer},
            {EResource.Gold, EJobType.Seller},
            {EResource.Shiny, EJobType.Priest}
        };
        public readonly Dictionary<EJobType, EResource> jobToResource =  new Dictionary<EJobType, EResource>()
        {
            {EJobType.Lumberjack, EResource.Wood},
            {EJobType.Miner, EResource.Stone},
            {EJobType.Farmer, EResource.Food},
            {EJobType.Seller, EResource.Gold},
            {EJobType.Priest, EResource.Shiny}
        };
        public readonly Dictionary<EResource, EBuilding> resourceToBuilding =  new Dictionary<EResource, EBuilding>()
        {
            {EResource.Wood, EBuilding.TimberLodger},
            {EResource.Stone, EBuilding.PebbleTemple},
            {EResource.Food, EBuilding.BreadShed},
            {EResource.Gold, EBuilding.CashStash},
            {EResource.Shiny, EBuilding.ShineShrine}
        };
        public readonly Dictionary<EBuilding, EResource> buildingToResource =  new Dictionary<EBuilding, EResource>()
        {
            {EBuilding.TimberLodger, EResource.Wood},
            {EBuilding.PebbleTemple, EResource.Stone},
            {EBuilding.BreadShed, EResource.Food},
            {EBuilding.CashStash, EResource.Gold},
            {EBuilding.ShineShrine, EResource.Shiny}
        };
        public readonly Dictionary<EBuilding, EJobType> buildingToJob =  new Dictionary<EBuilding, EJobType>()
        {
            {EBuilding.TimberLodger, EJobType.Lumberjack},
            {EBuilding.PebbleTemple, EJobType.Miner},
            {EBuilding.BreadShed, EJobType.Farmer},
            {EBuilding.CashStash, EJobType.Seller},
            {EBuilding.ShineShrine, EJobType.Priest}
        };
        public readonly Dictionary<EJobType, EBuilding> jobToBuilding =  new Dictionary<EJobType, EBuilding>()
        {
            {EJobType.Lumberjack, EBuilding.TimberLodger},
            { EJobType.Miner,  EBuilding.PebbleTemple},
            {EJobType.Farmer, EBuilding.BreadShed},
            {EJobType.Seller, EBuilding.CashStash},
            { EJobType.Priest, EBuilding.ShineShrine}
        };

        public List<Enemy> Enemies = new List<Enemy>();
        
        public Camera mainCamera;

        public void RebuildNavmesh()
        {
            navMesh.BuildNavMesh();
        }

        private void Start()
        {
            start = DateTime.Now;
        }

        public string GetGameTimeString()
        {
            TimeSpan currentGameTime = GameTime + (DateTime.Now - start); 
            
            return currentGameTime.ToString(@"hh\:mm\:ss");
        }

        public TimeSpan GetGameTime()
        {
            return GameTime + (DateTime.Now - start);
        }
        
        private void Destroy()
        {
            DateTime end = DateTime.Now;

            if (GameTime.TotalSeconds <= 0)
            {
                GameTime = end - start;
            }
            else
            {
                GameTime += end - start;
            }
        }

        public VillageUnitCombatStats GetCombatStats(ECombatArchetype eCombatType)
        {
            return villageUnitCombatStats.Find(stats => stats.eCombatType == eCombatType);
        }

        public VillageUnit GetArchetypePrefab(ECombatArchetype eCombatType)
        {
            return archetypePrefabs.Find(prefab => prefab.archetype == eCombatType ).prefab;
        }
    }
}