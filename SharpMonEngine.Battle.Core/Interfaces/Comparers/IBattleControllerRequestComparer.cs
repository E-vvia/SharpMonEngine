using System;
using SharpMonEngine.Battle.Core.Model.Controller;
using SharpMonEngine.Battle.Core.Request.Controller;

namespace SharpMonEngine.Battle.Core.Interfaces.Comparers
{
    public interface IBattleControllerRequestComparer
    {
        public Comparison<BattleControllerRequest> GetComparer(BattleInstance battleInstance);
    }
}