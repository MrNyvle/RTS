using System;
using _Scripts.UI.Resources;
using _Scripts.Unit;
using UnityEngine;

namespace _Scripts
{
    [Serializable]
    public class ResourceImage
    {
        public EResource resource;
        public Sprite sprite;
    }

    [Serializable]
    public class ResourceUIResourceItem
    {
        public EResource resource ; 
        public ResourceItem  resourceItem;
    }

    [Serializable]
    public class ResourcesCost
    {
        public int gold;
        public int wood;
        public int stone;
        public float buildTime;

        public int[] GetResourcesArray => new int[]{gold, stone, wood};

        public EResource GetResource(int index)
        {
            return new EResource[] { EResource.Gold, EResource.Stone, EResource.Wood }[index];
        }

        public void Deposit(EResource resource, int quantity)
        {
            switch (resource)
            {
                case EResource.Gold:
                    gold += quantity;
                    break;
                case EResource.Stone:
                    stone += quantity;
                    break;
                case EResource.Wood:
                    wood += quantity;
                    break;
            }
        }
    }
}