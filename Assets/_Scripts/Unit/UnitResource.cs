using System.Collections.Generic;

namespace _Scripts.Unit
{
    public class UnitResource
    {
        private Dictionary<Resource, int> resourcesTransported;

        public void AddResourceToInv(Resource resource, int quantity)
        {
            resourcesTransported.Add(resource, quantity);
        }
        
        public (Resource, int) RemoveResourceFromInv(Resource resource)
        {
            int quantity = resourcesTransported[resource];
            resourcesTransported.Remove(resource);
            return (resource, quantity);
        }
    }
}