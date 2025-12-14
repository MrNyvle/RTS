using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.UI.Resources;
using _Scripts.Unit;
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
            }
        }
        
        public void UpdateUI(UnitResource resource)
        {
            foreach (var kvp in _resourcesItemsUI)
            {
                if (resource.resourcesTransported.ContainsKey(kvp.Key))
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