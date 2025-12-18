using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Scripts.UI;
using _Scripts.Unit;
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
        
        private void Awake()
        {
            foreach (var eBuildingBuilding in buildingList)
            {
                buildings.Add(eBuildingBuilding.eBuilding, eBuildingBuilding.building);
            }
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

        public bool GetBuilding<T>(out T building) where T : Building
        {
            List<Building> buildingList =  buildings.Values.ToList();

            foreach (Building b in buildingList)
            {
                if (b.GetType() == typeof(T))
                {
                    building = b as T;
                    return true;
                }
            }
            building = null;
            return false;
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

        
        public EBuilding GetBuildingType(EResource resource)
        {
            GameManager.Instance.resourceToBuilding.TryGetValue(resource, out var building);
            return building;
        }

        public void UpdateUI()
        {
            UiManager.Instance.UpdateResourceBar(this);
        }
    }
}