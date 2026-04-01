using System;
using System.Collections.Generic;
using _Scripts.AntiCheat;
using _Scripts.UI;
using _Scripts.Unit;

namespace _Scripts.Buildings
{
    [Serializable]
    public class BuildingResource
    {
        private Dictionary<EResource, SecureInt> _resources = new ();
        public List<EResource> acceptedResources = new();

        public bool DepositResource(EResource type, int amount)
        {
            if (!acceptedResources.Contains(type))
                return false;

            if (!_resources.ContainsKey(type))
            {
                _resources[type] = new SecureInt(amount);
            }
            else
            {
                _resources[type].SetValue(_resources[type].GetValue() + amount);
            }
            return true;
        }
        
        public bool UseResource(EResource resource, int quantity)
        {
            if (quantity < 0)
            {
                AntiCheatManager.Instance.FlagCheat("Negative quantity of resource used");
                return false;
            }
                
            
            if (_resources.ContainsKey(resource) && _resources[resource].GetValue() >= quantity)
            {
                _resources[resource].SetValue(_resources[resource].GetValue() - quantity);
                return true;
            }
            return false;
        }
        
        public int GetResourceQuantity(EResource resource)
        {
            if (!_resources.ContainsKey(resource))
                return 0;
            
            return _resources[resource].GetValue();
        }

        public Dictionary<EResource, SecureInt> GetResources()
        {
            return _resources;
        }
    }
}