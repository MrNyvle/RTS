using _Scripts.Unit;
using UnityEngine;
using UnityEngine.Serialization;

namespace _ScriptableObjects.VillageUnit
{
    [CreateAssetMenu(fileName = "VillageUnitCombatStats", menuName = "Scriptable Objects/VillageUnitCombatStats")]
    public class VillageUnitCombatStats : ScriptableObject
    {
        public ECombatArchetype eCombatType;
        
        public int attackPoints;
        public int attackSpeedPoints;
        public int healthPoints;
        public int rangePoints;
    }
}
