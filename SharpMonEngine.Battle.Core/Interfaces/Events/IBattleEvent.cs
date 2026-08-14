using SharpMonEngine.Battle.Core.Events;

namespace SharpMonEngine.Battle.Core.Interfaces.Events
{
    public interface IBattleEvent
    {
        public BattleEventType EventType { get; }
    }
}