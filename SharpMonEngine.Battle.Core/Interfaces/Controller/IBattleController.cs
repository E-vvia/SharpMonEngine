using System.Collections.Generic;
using SharpMonEngine.Battle.Core.Model.Controller;
using SharpMonEngine.Battle.Core.Request.Controller;
using SharpMonEngine.Battle.Core.Result.Controller;

namespace SharpMonEngine.Battle.Core.Interfaces.Controller
{
    public interface IBattleController
    {
        BattleControllerResult InitializeBattle(BattleInstance battleInstance);

        BattleControllerResult DoAction(BattleInstance battleInstance,
            IEnumerable<BattleControllerRequest> battleControllerRequests);
    }
}