using System.Collections.Generic;
using System.Linq;
using _Scripts;
using _Scripts.Unit;
using UnityEngine;

namespace _ScriptableObjects.UI
{
    [CreateAssetMenu(fileName = "IconUI", menuName = "Scriptable Objects/IconUI")]
    public class IconsUI : ScriptableObject
    {
        public List<ResourceImage> resourceIcons;

        public Sprite GetResourceSprite(EResource resource)
        {
            return resourceIcons.First(n => n.resource == resource).sprite;
        }
        
    }
}