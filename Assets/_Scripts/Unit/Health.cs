using System;
using _Scripts.Enemies;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Scripts.Unit
{
    public class Health : MonoBehaviour
    {
        float _maxHealth = 10;
        public float healthPoints = 5;
        public UnityEvent onDeath;
        public Slider healthSlider;
        
        private UnitStats _unitStats;
        
        private void Start()
        {
            TryGetComponent(out VillageUnit villageUnit);
            TryGetComponent(out Enemy enemy);

            if (enemy == null && villageUnit == null)
            {
                Debug.LogWarning("Game Object does not contain a Village Unit or Enemy component."); 
            }
            else
            {
                UnitStats stats = enemy ==null ? villageUnit.unitStats : enemy.unitStats ;
                _maxHealth = stats.GetCombatStat(ECombatStat.HealthPoints);
            }
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
            UpdateHealthBar();
            if (healthPoints == 0)
                onDeath.Invoke();
        }

        public void Heal(int amount)
        {
            healthPoints = Mathf.Clamp(healthPoints += amount, 0, _maxHealth);
        }
        
        private void UpdateHealthBar()
        {
            if (healthSlider is null) return;
            healthSlider.value = healthPoints / _maxHealth;
        }
        
    }
}