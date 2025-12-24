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
        public int id;
        
        public Dictionary<EBuilding, Building> buildings = new () ;

        public List<EBuildingBuilding> buildingList =  new List<EBuildingBuilding>();
        
        public List<VillageUnit> villageUnits = new List<VillageUnit>();

        public VillageUnit villageUnitPrefab;
        
        private void Awake()
        {
            foreach (var eBuildingBuilding in buildingList)
            {
                buildings.Add(eBuildingBuilding.eBuilding, eBuildingBuilding.building);
            }
        }

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
            
            foreach (KeyValuePair<EBuilding, Building> building in buildings)
            {
                foreach (var kp in building.Value.buildingResource._resources)
                {
                    villageResources[kp.Key] = villageResources.GetValueOrDefault(kp.Key) +  kp.Value;
                }
            }
            
            return villageResources;
        }
        
        public Building GetBuildingForResource(EResource resource)
        {
            foreach (var kp in buildings)
            {
                if (kp.Value.buildingResource.acceptedResources.Contains(resource))
                {
                    return kp.Value;
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