using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Buildings;
using _Scripts.Unit;
using UnityEngine;

namespace _Scripts.UI.Resources
{
    public class ResourceBar : MonoBehaviour
    {
        TownHall _townHall;

        public List<ResourceUIResourceItem> resourcesItems = new ();

        private void Start()
        {
            foreach (var kvp in resourcesItems)
            {
                kvp.resourceItem.SetItemUI(kvp.resource, 0);
            }
        }

        public ResourceItem GetResourceUI(EResource resource)
        {
            return resourcesItems.First(x => x.resource == resource).resourceItem;
        }
    }
}