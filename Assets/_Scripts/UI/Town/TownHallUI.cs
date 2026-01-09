using _Scripts.Buildings;
using _Scripts.Unit;
using TMPro;
using UnityEngine.UIElements;

namespace _Scripts.UI
{
    public class TownHallUI : BuildingUI
    {
        public override EBuilding BuildingType => EBuilding.TownHall;
        
        private TownHall _townHall;

        public override void FillUI(Building building)
        {
            _townHall = building as TownHall;
            buildingName.text = _townHall?.name;
        }

        public void SpawnUnit()
        {
            _townHall.SpawnVillageUnit();
        }
    }
}