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