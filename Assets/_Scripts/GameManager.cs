using System.Collections.Generic;
using _ScriptableObjects.VillageUnit;
using _Scripts.Unit;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        public VillageUnit SelectedUnit { get; set; }
        public List<VillageUnitCombatStats> villageUnitCombatStats;
        public Camera mainCamera;
        

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            
            DontDestroyOnLoad(gameObject);
        }

        public VillageUnitCombatStats GetCombatStats(CombatType combatType)
        {
            return villageUnitCombatStats.Find(stats => stats.combatType == combatType);
        }
        
        
        private void Start()
        {}
        
        
    }
}