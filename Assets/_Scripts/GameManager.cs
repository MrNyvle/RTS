using System.Collections.Generic;
using _ScriptableObjects.VillageUnit;
using _Scripts.Unit;
using UnityEngine;

namespace _Scripts
{
    public class GameManager : Singleton<GameManager>
    {
        public VillageUnit SelectedUnit { get; set; }
        public List<VillageUnitCombatStats> villageUnitCombatStats;
        public Camera mainCamera;
        

        public VillageUnitCombatStats GetCombatStats(ECombatType eCombatType)
        {
            return villageUnitCombatStats.Find(stats => stats.eCombatType == eCombatType);
        }
    }
}