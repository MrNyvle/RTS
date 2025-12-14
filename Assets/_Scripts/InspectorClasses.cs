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
}