using SharpMonEngine.Battle.Core.Interfaces.Events;

namespace SharpMonEngine.Battle.Core.Events
{
    public class WithdrawBattleEvent : IBattleEvent
    {
        public BattleEventType EventType { get; } = BattleEventType.Withdraw;
    }
}