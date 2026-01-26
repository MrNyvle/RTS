using System;
using System.Collections.Generic;
using _Scripts.Unit;
using UnityEngine;

namespace _ScriptableObjects.VillageUnit
{
    [Serializable]
    public class ArchetypePrice
    {
        public ECombatArchetype resource;
        public int price;
        public float time;
    }
    
    [CreateAssetMenu(fileName = "VillageUnitPrice", menuName = "Scriptable Objects/VillageUnitPrice")]
    public class VillageUnitPrice : ScriptableObject
    {
        public List<ArchetypePrice> prices;
        
        public int GetPrice(ECombatArchetype archetype)
        {
            return prices.Find(p => p.resource == archetype).price;
        }
        public float GetTime(ECombatArchetype archetype)
        {
            return prices.Find(p => p.resource == archetype).time;
        }
    }
}