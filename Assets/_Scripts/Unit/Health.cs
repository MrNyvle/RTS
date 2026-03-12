using System;
using _Scripts.Enemies;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace _Scripts.Unit
{
    public class Health : MonoBehaviour
    {
        float _maxHealth = 10;
        [ReadOnly]
        public float healthPoints = 5;
        public UnityEvent onDeath;
        private UnitStats _unitStats;
        
        private void Start()
        {
            TryGetComponent(out VillageUnit villageUnit);
            TryGetComponent(out Enemy enemy);
            
            onDeath.AddListener(() => Destroy(gameObject));

            if (enemy == null && villageUnit == null)
            {
                Debug.LogWarning("Game Object does not contain a Village Unit or Enemy component.");
            }

            UnitStats stats = enemy ==null ? villageUnit.unitStats : enemy.unitStats ;
            
            _maxHealth = stats.GetCombatStat(ECombatStat.HealthPoints);
        }

        public bool IsDead()
        {
            return healthPoints <= 0;
        }
        
        public void Damage(float amount)
        {
            Debug.Log("Damage: " + amount);
            healthPoints -= amount;
            healthPoints = Mathf.Clamp(healthPoints, 0, _maxHealth);
            if (healthPoints == 0)
                onDeath.Invoke();
        }

        public void Heal(int amount)
        {
            healthPoints = Mathf.Clamp(healthPoints += amount, 0, _maxHealth);
        }
    }
}