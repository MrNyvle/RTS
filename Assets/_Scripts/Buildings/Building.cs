using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Unit;
using UnityEngine;
using _ScriptableObjects.Building;
using UnityEngine.Serialization;
using UnityEngine.UI;
using _Scripts.UI;

namespace _Scripts.Buildings
{
    public enum EBuildingState
    {
        Built,
        Building,
        Unbuilt,
    }
    
    [Serializable]
    public abstract class Building : MonoBehaviour
    {
        public virtual EBuilding EBuildingType { get; set; }
        public TownHall townHall;
        public GameObject entrance;
        public GameObject builtModel;
        public GameObject unBuiltModel;
        public BuildingResource resource;
        public BuildingStats stats;
        public float depositTime;
        public bool isBuilt;

        public BuildTimerUI timerUI;


        private List<ResourcesCost> _currentBuildingResource = new List<ResourcesCost>(){new ResourcesCost(), new ResourcesCost(), new ResourcesCost(), new ResourcesCost()};
        public int currentTier = 0;

        public Vector3 GetEntrancePosition()
        {
            return entrance.transform.position;
        }

        public EResource? GetNeededBuildingResource(out int quantity)
        {
            int[] currentResources = _currentBuildingResource[currentTier].GetResourcesArray;
            int[] neededResources = stats.tiers[currentTier].GetResourcesArray;

            for (int i = 0; i < currentResources.Length; i++)
            {
                if (currentResources[i] != neededResources[i])
                {
                    quantity = neededResources[i];
                    return stats.tiers[currentTier].GetResource(i);
                }
            }
            quantity = 0;
            return null;
        }
        
        public void DepositBuildingResource(EResource res, int quantity)
        {
            _currentBuildingResource[currentTier].Deposit(res, quantity);
        }
        
        public void Build()
        {
            isBuilt = true;
            unBuiltModel.SetActive(false);
            builtModel.SetActive(true);
        }
    }
}