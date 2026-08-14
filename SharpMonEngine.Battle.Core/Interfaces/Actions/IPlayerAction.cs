using SharpMonEngine.Battle.Core.Actions;
using SharpMonEngine.Battle.Core.Model;

namespace SharpMonEngine.Battle.Core.Interfaces.Actions
{
    public interface IPlayerAction
    {
        public int Side { get; }
        public int Slot { get; }
        public PlayerActionType ActionType { get; }
        public void Resolve(BattleInstance battleInstance);
    }
}