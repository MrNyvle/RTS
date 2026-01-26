using System.Collections;
using System.Collections.Generic;
using _Scripts.UI;
using _Scripts.Unit;
using UnityEngine;
using UnityEngine.Video;

namespace _Scripts.Buildings
{
    public class SoldierCoffer : Building
    {
        public override EBuilding EBuildingType => EBuilding.SoldierCoffer;

        public void MakeMilitaryUnit(ECombatArchetype eCombatArchetype)
        {
            int unitPrice = GameManager.Instance.villageUnitPrice.GetPrice(eCombatArchetype);
            
            List<(EResource, int)> prices = new List<(EResource, int)>(){ (EResource.Gold,unitPrice)};
            
            bool canAfford = townHall.CanAfford(prices);
            if (!canAfford) return;
            townHall.UseResources(prices);

            StartCoroutine(MakeMilitaryUnitCoroutine(GameManager.Instance.villageUnitPrice.GetTime(eCombatArchetype), eCombatArchetype));
        }

        private IEnumerator MakeMilitaryUnitCoroutine(float time, ECombatArchetype eCombatArchetype)
        {
            yield return new WaitForSeconds(time);
            SpawnMilitaryUnit(eCombatArchetype);
        }
        
        private void SpawnMilitaryUnit(ECombatArchetype eCombatArchetype)
        {
            VillageUnit prefab = GameManager.Instance.GetArchetypePrefab(eCombatArchetype);
            VillageUnit villageUnit = Instantiate(prefab, GetEntrancePosition(), Quaternion.identity);
            villageUnit.AssignTownHall(townHall);
            townHall.villageUnits.Add(villageUnit);
        }
    }
}