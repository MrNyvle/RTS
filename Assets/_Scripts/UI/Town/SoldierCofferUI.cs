using _Scripts.Buildings;
using _Scripts.Unit;

namespace _Scripts.UI
{
    public class SoldierCofferUI : BuildingUI
    {
        public override EBuilding BuildingType => EBuilding.SoldierCoffer;
        private SoldierCoffer _soldierCoffer;
        
        public override void FillUI(Building building)
        {
            _soldierCoffer = building as SoldierCoffer;
            buildingName.text = _soldierCoffer?.name;
        }

        public void MakeBasicUnit()
        {
            _soldierCoffer.MakeMilitaryUnit(ECombatArchetype.Basic);
        }
        public void MakeTank(){}
        public void MakeAssassin(){}
    }
}