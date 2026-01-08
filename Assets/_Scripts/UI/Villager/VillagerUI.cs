using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.UI.Resources;
using _Scripts.Unit;
using Unity.VisualScripting;
using UnityEngine;

namespace _Scripts.UI.Villager
{
    public class VillagerUI : MonoBehaviour
    {
        public List<ResourceItem> items;
        private Dictionary<EResource, ResourceItem> _resourcesItemsUI =  new ();

        private void Start()
        {
            for (int i = 0; i < items.Count; i++)
            {
                _resourcesItemsUI.Add((EResource)i, items[i]);
                items[i].gameObject.SetActive(false);
            }
        }
        
        public void UpdateUI(UnitResource resource)
        {
            if (resource.resourcesTransported.Count == 0)
            {
                foreach (KeyValuePair<EResource, ResourceItem> kvp in _resourcesItemsUI)
                {
                    kvp.Value.gameObject.SetActive(false);
                    return;
                }
            }
            
            foreach (var kvp in _resourcesItemsUI)
            {
                if (resource.resourcesTransported.ContainsKey(kvp.Key) && resource.resourcesTransported[kvp.Key] != 0 )
                {
                    kvp.Value.SetItemUI(kvp.Key, resource.resourcesTransported[kvp.Key]);
                    kvp.Value.gameObject.SetActive(true);
                }
                else
                {
                    kvp.Value.gameObject.SetActive(false);
                }
            }
        }
    }
}