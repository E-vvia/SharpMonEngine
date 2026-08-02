using System.Collections.Generic;
using SharpMonEngine.Battle.Core.Model.Controller;

namespace SharpMonEngine.Battle.Core.Result.Controller
{
    public class BattleControllerResult
    {
        public BattleState PreviousState { get; set; }
        public BattleState CurrentState { get; set; }
        public List<BattleControllerResultEvent> Events { get; set; } = new List<BattleControllerResultEvent>();
    }
}