namespace _Scripts.Unit
{
    public class UnitCombatStats
    {
        private int attackPoints;
        private int attackSpeedPoints;
        private int healthPoints;
        private int rangePoints;

        public UnitCombatStats(CombatType combatType, int warriorPoints)
        {
            attackPoints = GameManager.Instance.GetCombatStats(combatType).attackPoints *  warriorPoints/10;
            attackSpeedPoints = GameManager.Instance.GetCombatStats(combatType).attackSpeedPoints *  warriorPoints/10;
            healthPoints = GameManager.Instance.GetCombatStats(combatType).healthPoints *  warriorPoints/10;
            rangePoints = GameManager.Instance.GetCombatStats(combatType).rangePoints *  warriorPoints/10;
        }
    }
}