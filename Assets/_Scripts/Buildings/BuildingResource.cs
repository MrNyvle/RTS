using System;
using System.Collections.Generic;
using _Scripts.UI;
using _Scripts.Unit;

namespace _Scripts.Buildings
{
    [Serializable]
    public class BuildingResource
    {
        private Dictionary<EResource, int> _resources = new ();
        public List<EResource> acceptedResources = new();
        
        public void Deposit(EResource type, VillageUnit unit)
        {
                
        }

        public bool DepositResource(EResource type, int amount)
        {
            if (!acceptedResources.Contains(type))
                return false;
            
            _resources[type] = _resources.GetValueOrDefault(type) + amount;
            
            return true;
        }
        
        public bool UseResource(EResource resource, int quantity)
        {
            if (_resources.ContainsKey(resource) && _resources[resource] >= quantity)
            {
                _resources[resource] -= quantity;
                return true;
            }
            return false;
        }
        
        public int GetResourceQuantity(EResource resource)
        {
            return _resources.GetValueOrDefault(resource);
        }

        public Dictionary<EResource, int> GetResources()
        {
            return _resources;
        }
    }
}