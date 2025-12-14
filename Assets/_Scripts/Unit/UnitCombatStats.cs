namespace _Scripts.Unit
{
    public class UnitCombatStats
    {
        private int attackPoints;
        private int attackSpeedPoints;
        private int healthPoints;
        private int rangePoints;

        public UnitCombatStats(ECombatType eCombatType, int warriorPoints)
        {
            attackPoints = GameManager.Instance.GetCombatStats(eCombatType).attackPoints *  warriorPoints/10;
            attackSpeedPoints = GameManager.Instance.GetCombatStats(eCombatType).attackSpeedPoints *  warriorPoints/10;
            healthPoints = GameManager.Instance.GetCombatStats(eCombatType).healthPoints *  warriorPoints/10;
            rangePoints = GameManager.Instance.GetCombatStats(eCombatType).rangePoints *  warriorPoints/10;
        }
    }
}