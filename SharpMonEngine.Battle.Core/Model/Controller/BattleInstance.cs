namespace SharpMonEngine.Battle.Core.Model.Controller
{
    public class BattleInstance
    {
        public int Sides { get; set; }
        public int CombatantBySide { get; set; }
        public SpeciesBattleInstance[,] Combatants { get; set; } = new SpeciesBattleInstance[0, 0];
        public BattleState BattleState { get; set; }
        public int TurnNumber { get; set; } = 0;
    }
}