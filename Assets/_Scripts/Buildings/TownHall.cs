using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Scripts.UI;
using _Scripts.Unit;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Buildings
{
    [Serializable]
    public class EBuildingBuilding
    {
        public EBuilding eBuilding;
        public Building building;
    }
    public class TownHall :  Building
    {
        public override EBuilding eBuildingType => EBuilding.TownHall;
        public int id;
        
        public List<Building> buildings=  new List<Building>();
        public List<VillageUnit> villageUnits = new List<VillageUnit>();
        public VillageUnit villageUnitPrefab;

        [Button]
        public void SpawnVillageUnit()
        {
            VillageUnit villageUnit = Instantiate(villageUnitPrefab, GetEntrancePosition(), Quaternion.identity);
            villageUnit.AssignTownHall(this);
            villageUnits.Add(villageUnit);
        }

        public float AvgTownCombatLevel()
        {
            float sum = 0;
            foreach (VillageUnit villageUnit in villageUnits)
            {
                sum += villageUnit.unitStats.jobsPoint[EJobType.Warrior];
            }
            float avg = sum / villageUnits.Count;

            return avg;
        }
        
        public void AssignBuildings(Building building)
        {
            Dictionary<EBuilding, Building> buildings = new();
        }

        public Dictionary<EResource, int> GetVillageResources()
        {
            Dictionary<EResource, int> villageResources = new Dictionary<EResource, int>();
            
            foreach (Building building in buildings)
            {
                foreach (var kp in building.buildingResource._resources)
                {
                    villageResources[kp.Key] = villageResources.GetValueOrDefault(kp.Key) +  kp.Value;
                }
            }
            
            return villageResources;
        }
        
        public Building GetBuildingForResource(EResource resource)
        {
            foreach (Building building in buildings)
            {
                if (building.buildingResource.acceptedResources.Contains(resource))
                {
                    return building;
                }
            }
            return null;
        }
        
        public void UpdateUI()
        {
            UiManager.Instance.UpdateResourceBar(this);
        }
    }
}