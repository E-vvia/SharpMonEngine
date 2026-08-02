using System;

namespace SharpMonEngine.Battle.Core.Request.Controller
{
    public class BattleControllerRequest
    {
        public BattleControllerRequestType InputType { get; set; }
        public int Slot { get; set; }
        public int Side { get; set; }
        public int[] Args { get; set; } = Array.Empty<int>();
        public (int, int)[] Targets { get; set; } = Array.Empty<(int, int)>();
    }
}