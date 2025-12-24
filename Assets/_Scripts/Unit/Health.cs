using System;
using _Scripts.Enemies;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace _Scripts.Unit
{
    public class Health : MonoBehaviour
    {
        int _maxHealth = 10;
        int _healthPoints = 5;
        public UnityEvent onDeath;
        private UnitStats _unitStats;
        
        private void Start()
        {
            TryGetComponent(out VillageUnit villageUnit);
            TryGetComponent(out Enemy enemy);

            if (enemy == null && villageUnit == null)
            {
                Debug.LogWarning("Game Object does not contain a Village Unit or Enemy component.");
            }

            UnitStats stats = enemy ==null ? villageUnit.unitStats : enemy.unitStats ;
            
            _maxHealth = stats.GetCombatStat(ECombatStat.HealthPoints);
        }
        
        public void Damage(int amount)
        {
            Mathf.Clamp(_healthPoints -= amount, 0, _maxHealth);
            if (_healthPoints == 0)
                onDeath.Invoke();
        }

        public void Heal(int amount)
        {
            _healthPoints = Mathf.Clamp(_healthPoints += amount, 0, _maxHealth);
        }
    }
}