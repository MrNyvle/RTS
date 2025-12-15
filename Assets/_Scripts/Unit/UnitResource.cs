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
        
        public void Clear()
        {
            resourcesTransported.Clear();
        }
    }
}