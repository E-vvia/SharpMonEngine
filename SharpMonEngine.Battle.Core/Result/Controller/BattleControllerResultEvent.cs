using System;

namespace SharpMonEngine.Battle.Core.Result.Controller
{
    public class BattleControllerResultEvent
    {
        public enum EventType
        {
            DamageDone
        }

        public EventType EventResultType { get; set; }
        public (int, int) Source { get; set; }
        public (int, int) Target { get; set; }
        public int[] Args { get; set; } = Array.Empty<int>();
    }
}