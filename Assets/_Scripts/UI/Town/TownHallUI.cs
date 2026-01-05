using _Scripts.Buildings;
using _Scripts.Unit;
using TMPro;
using UnityEngine.UIElements;

namespace _Scripts.UI
{
    public class TownHallUI : BuildingUI
    {
        public new EBuilding BuildingType
        {
            get => EBuilding.TownHall;
            set => buildingType = value;
        }
        
        public TextMeshProUGUI buildingName;
        private TownHall _townHall;

        public void FillTownHallUI(TownHall townHall)
        {
            _townHall = townHall;
            buildingName.text = townHall.name;
        }

        public void SpawnUnit()
        {
            _townHall.SpawnVillageUnit();
        }
    }
}