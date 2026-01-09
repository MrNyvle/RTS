using _Scripts.Buildings;
using _Scripts.Unit;
using TMPro;

namespace _Scripts.UI
{
    public class TimberLodgerUI : BuildingUI
    {
        public override EBuilding BuildingType => EBuilding.TimberLodger;

        private TimberLodger _timberLodger;

        public override void FillUI(Building building)
        {
            _timberLodger = building as TimberLodger;
            buildingName.text = _timberLodger?.name;
        }
    }
}