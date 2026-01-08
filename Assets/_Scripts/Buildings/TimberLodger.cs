using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Unit;

namespace _Scripts.Buildings
{
    public class TimberLodger : Building
    {
        public override EBuilding EBuildingType => EBuilding.TimberLodger;

        public void DepositWood(VillageUnit unit)
        {
            resource.DepositResource(EResource.Wood,unit.resource.GetResourceQuantity(EResource.Wood));
            unit.resource.ClearResource(EResource.Wood);
        }

        public void UseWood(int quantity)
        {
            resource.UseResource(EResource.Wood, quantity);
        }
    }
}