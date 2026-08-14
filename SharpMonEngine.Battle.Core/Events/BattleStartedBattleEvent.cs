using SharpMonEngine.Battle.Core.Interfaces.Events;

namespace SharpMonEngine.Battle.Core.Events
{
    public class BattleStartedBattleEvent : IBattleEvent
    {
        public BattleEventType EventType { get; } = BattleEventType.Start;
    }
}