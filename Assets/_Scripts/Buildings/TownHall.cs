using System.Collections.Generic;
using System.Threading.Tasks;
using _Scripts.UI;
using _Scripts.Unit;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Buildings
{
    public class TownHall :  MonoBehaviour
    {
        public int id;
        
        public Dictionary<EResource, int> _resources = new ();
        
        public GameObject entrance;
        public int depositTime;

        public Vector3 GetEntrancePosition()
        {
            return entrance.transform.position;
        }

        public void Deposit(VillageUnit unit)
        {
            foreach (var kvp in unit.unitResource.resourcesTransported)
            {
                unit.townHall.DepositResource(kvp.Key, kvp.Value);
            }
            unit.ClearResources();
        }
        
        public void DepositResource(EResource type, int amount)
        {
            _resources.TryGetValue(type, out var quantity);
            _resources[type] = quantity + amount;
            
            UiManager.Instance.UpdateResourceBar(this);
        }
    }
}