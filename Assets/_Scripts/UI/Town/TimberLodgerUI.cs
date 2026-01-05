using _Scripts.Buildings;
using _Scripts.Unit;
using TMPro;

namespace _Scripts.UI
{
    public class TimberLodgerUI : BuildingUI
    {
        public new EBuilding BuildingType => EBuilding.TimberLodger;

        private TimberLodger _timberLodger;
        public TextMeshProUGUI buildingName;
        
        public void FillTimberLodgerUI(TimberLodger timberLodger)
        {
            _timberLodger = timberLodger;
            buildingName.text = timberLodger.name;
        }
        
    }
}