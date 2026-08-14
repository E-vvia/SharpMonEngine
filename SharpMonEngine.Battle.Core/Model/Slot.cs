using SharpMonEngine.Battle.Core.Actions;

namespace SharpMonEngine.Battle.Core.Model
{
    public class Slot
    {
        public int Id { get; set; }
        public Side Side { get; set; } = null!;
        public SpeciesBattleInstance? SpeciesBattleInstance { get; set; } = null;
        public PlayerActionType RequestedPlayerActionType { get; set; }

        public bool IsValidAction(PlayerActionType actionType)
        {
            return RequestedPlayerActionType.HasFlag(actionType);
        }

        public void SetCombatant(SpeciesBattleInstance instance)
        {
            SpeciesBattleInstance = instance;
        }
    }
}