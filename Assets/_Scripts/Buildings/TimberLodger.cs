using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Unit;

namespace _Scripts.Buildings
{
    public class TimberLodger : Building
    {
        public override EBuilding eBuildingType => EBuilding.TimberLodger;

        public void DepositWood(VillageUnit unit)
        {
            buildingResource.DepositResource(EResource.Wood,unit.unitResource.GetResourceQuantity(EResource.Wood));
            unit.unitResource.ClearResource(EResource.Wood);
        }

        public void UseWood(int quantity)
        {
            buildingResource.UseResource(EResource.Wood, quantity);
        }
    }
}