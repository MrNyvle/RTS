using System;
using System.Collections.Generic;
using _Scripts.Resource;
using _Scripts.UI;
using _Scripts.Unit;
using NaughtyAttributes;
using UnityEngine;

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
        public override EBuilding EBuildingType => EBuilding.TownHall;
        public int id;
        
        public List<Building> buildings=  new List<Building>();
        public List<VillageUnit> villageUnits = new List<VillageUnit>();
        public VillageUnit villageUnitPrefab;

        public float buildRange = 10;
        
        [Button]
        public void SpawnVillageUnit()
        {
            VillageUnit villageUnit = Instantiate(villageUnitPrefab, GetEntrancePosition(), Quaternion.identity);
            villageUnit.AssignTownHall(this);
            villageUnits.Add(villageUnit);
        }

        [Button]
        public void GiveResources()
        {
            Building g = GetBuildingForResource(EResource.Gold);
            Building s = GetBuildingForResource(EResource.Stone);
            Building w = GetBuildingForResource(EResource.Wood);
            if (g)
                g.resource.DepositResource(EResource.Gold, 10);
            else
                resource.DepositResource(EResource.Gold, 10);
            
            if (s)
                s.resource.DepositResource(EResource.Stone,10);
            else
                resource.DepositResource(EResource.Stone, 10);
            
            if (w)
                w.resource.DepositResource(EResource.Wood,10);
            else
                resource.DepositResource(EResource.Wood, 10);
            
            UpdateUI();
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
        
        public void AssignBuilding(Building building)
        {
            building.townHall = this;
            buildings.Add(building);
        }

        public Dictionary<EResource, int> GetVillageResources()
        {
            Dictionary<EResource, int> villageResources = new Dictionary<EResource, int>();
            
            foreach (Building building in buildings)
            {
                foreach (var kp in building.resource.GetResources())
                {
                    villageResources[kp.Key] = villageResources.GetValueOrDefault(kp.Key) +  kp.Value;
                }
            }
            
            foreach (var kp in resource.GetResources())
            {
                villageResources[kp.Key] = villageResources.GetValueOrDefault(kp.Key) +  kp.Value;
            }
            
            return villageResources;
        }
        
        public Building GetBuildingForResource(EResource res)
        {
            foreach (Building building in buildings)
            {
                if (building.resource.acceptedResources.Contains(res))
                {
                    return building;
                }
            }
            return this;
        }

        public List<Building> GetBuildingsForResource(EResource res)
        {
            List<Building> bldngs = new List<Building>();
            foreach (Building building in buildings) {
                if (building.resource.acceptedResources.Contains(res))
                {
                    bldngs.Add(building);
                }
            }
            
            return bldngs;
        }

        public Building GetClosestToResource(List<Building> buildingList, GameResource res)
        {
            float distance = float.MaxValue;
            Building closest = null;
            foreach (var building in buildingList)
            {
                float dist = Vector3.Distance(building.GetEntrancePosition(), res.GetPosition());

                if (dist < distance)
                {
                    distance = dist;
                    closest = building;
                }
            }
            return closest;
        }

        public bool CanAfford(List<(EResource, int)> resources)
        {
            Dictionary<EResource, int> villageResources = GetVillageResources();
            
            foreach (var res in resources)
            {
                if (!villageResources.ContainsKey(res.Item1))
                    return false;
                if (villageResources[res.Item1] < res.Item2)
                    return false;
                
            }
            return true;
        }

        public void UseResources(List<(EResource, int)> resources)
        {
            foreach (var res in resources)
            {
                List<Building> buildings = GetBuildingsForResource(res.Item1);
                
                int resourceToUse = res.Item2;
                int buildingIndex = 0;
                
                while (resourceToUse > 0)
                {
                    if (resourceToUse < buildings[buildingIndex].resource.GetResources()[res.Item1])
                    {
                        buildings[buildingIndex].resource.UseResource(res.Item1, resourceToUse);
                        resourceToUse = 0;
                    }
                    else
                    {
                        resourceToUse -= buildings[buildingIndex].resource.GetResources()[res.Item1];
                        buildingIndex++;
                    }
                }
            }
            
            UpdateUI();
        }

        public void UpdateUI()
        {
            UiManager.Instance.UpdateResourceBar(this);
        }
    }
}