using System.Collections.Generic;
using _Scripts.Buildings;
using UnityEngine;

namespace _Scripts.Unit
{
    public class UnitResource
    {
        public Dictionary<EResource, int> resourcesTransported =  new Dictionary<EResource, int>();
        
        public void AddResourceToInv(EResource eResource, int quantity)
        {
            resourcesTransported[eResource] = resourcesTransported.GetValueOrDefault(eResource) + quantity;
        }

        public EResource GetMostResource(out int quantity)
        {
            KeyValuePair<EResource,int> rtrn = new KeyValuePair<EResource, int>();
            foreach (var kv in resourcesTransported)
            {
                if (kv.Value > rtrn.Value)
                {
                    rtrn = kv;
                }
            }
            quantity = rtrn.Value;
            return rtrn.Key;
        }

        public void DepositResourceToBuilding(BuildingResource resource)
        {
            List<EResource> depositedResources = new List<EResource>();
            foreach (var kv in resourcesTransported)
            {
                if (resource.acceptedResources.Contains(kv.Key))
                {
                    resource.DepositResource(kv.Key, kv.Value);
                    depositedResources.Add(kv.Key);
                }
            }

            foreach (var eResource in depositedResources)
            {
                ClearResource(eResource);
            }
        }
        
        public int GetResourceQuantity(EResource eResource)
        {
            return resourcesTransported.GetValueOrDefault(eResource);
        }

        public void ClearResource(EResource eResource)
        {
            resourcesTransported[eResource] = 0;
        }

        public bool RemoveResource(EResource eResource, int quantity)
        {
            if (resourcesTransported[eResource] - quantity < 0)
                return false;
            
            resourcesTransported[eResource] -= quantity;
            return true;
        }
        
        public void Clear()
        {
            resourcesTransported.Clear();
        }
    }
}