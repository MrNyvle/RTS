using System;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace _ScriptableObjects.Building
{
    
    
    [CreateAssetMenu(fileName = "BuildingStats", menuName = "Scriptable Objects/BuildingStats")]
    public class BuildingStats : ScriptableObject
    {
        public List<ResourcesCost> tiers = new List<ResourcesCost>();
        public int upkeepPrice;
    }
}

