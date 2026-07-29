using System;
using _Scripts.Unit;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

namespace _Scripts.Debugers
{
    [RequireComponent( typeof(VillageUnit))]
    public class UnitDEBUGER : MonoBehaviour
    {
     
        private VillageUnit unit;
        private Health health;
        private UnitMovement movement;
        private UnitStats stats;
        private UnitResource resource;

        [ResizableTextArea, ReadOnly, SerializeField]
        private String unitInfo;
        
        private String GetDebugInfo()
        {
            if (unit == null)
            {
                return "No Unit";
            }
            String inventory = "";
            foreach (var resource in resource.resourcesTransported)
            {
                inventory += $"{resource.Key.ToString()} : {resource.Value}\n";
            }
            
            
            return "--------------Health--------------\n" +
                   $"HP : {health.healthPoints}\n" +
                   $"Is Dead : {health.IsDead()}\n" +
                   "----------Combat Stats-----------\n" +
                   $"Combat Archetype : {stats.eCombatType.ToString()}\n" +
                   $"Attack Points: {stats.GetCombatStat(ECombatStat.AttackPoints)}\n" +
                   $"Attack Speed: {stats.GetCombatStat(ECombatStat.AttackSpeedPoints)}\n" +
                   $"DPS: {stats.GetCombatStat(ECombatStat.AttackPoints) * stats.GetCombatStat(ECombatStat.AttackSpeedPoints)}\n" +
                   "----------Resources--------------\n" +
                   $"{inventory}";

        }
        
        private void Start()
        {
            TryGetComponent(out unit);
            TryGetComponent(out health);
            TryGetComponent(out movement);
            stats = unit.unitStats;
            resource = unit.resource;
        }

        private void Update()
        {
            unitInfo = GetDebugInfo();
        }
    }
}