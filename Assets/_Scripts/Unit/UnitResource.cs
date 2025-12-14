using System.Collections.Generic;

namespace _Scripts.Unit
{
    public class UnitResource
    {
        public Dictionary<EResource, int> resourcesTransported =  new Dictionary<EResource, int>();
        
        public void AddResourceToInv(EResource eResource, int quantity)
        {
            resourcesTransported.TryGetValue(eResource, out int count);
            resourcesTransported[eResource] = count + quantity;
        }

        public void Clear()
        {
            resourcesTransported.Clear();
        }
    }
}