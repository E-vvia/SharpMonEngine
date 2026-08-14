using SharpMonEngine.Battle.Core.Interfaces.Actions;
using SharpMonEngine.Battle.Core.Model;

namespace SharpMonEngine.Battle.Core.Actions
{
    public class SendPlayerAction : IPlayerAction
    {
        public SpeciesBattleInstance Instance { get; set; } = null!;
        public int Side { get; set; }
        public int Slot { get; set; }
        public PlayerActionType ActionType => PlayerActionType.SendCombatant;

        public void Resolve(BattleInstance battleInstance)
        {
            battleInstance.SetCombatant(Instance, Side, Slot);
        }
    }
}