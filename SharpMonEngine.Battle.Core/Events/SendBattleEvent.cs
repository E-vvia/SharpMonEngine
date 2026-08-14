using SharpMonEngine.Battle.Core.Interfaces.Events;

namespace SharpMonEngine.Battle.Core.Events
{
    public class SendBattleEvent : IBattleEvent
    {
        public BattleEventType EventType { get; } = BattleEventType.Send;
    }
}