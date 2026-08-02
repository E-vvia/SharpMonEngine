using System;
using SharpMonEngine.Battle.Core.Interfaces.Comparers;
using SharpMonEngine.Battle.Core.Model;
using SharpMonEngine.Battle.Core.Model.Controller;
using SharpMonEngine.Battle.Core.Request.Controller;
using SharpMonEngine.Core.Interfaces.Providers;

namespace SharpMonEngine.Comparers
{
    public class DefaultRequestComparer : IBattleControllerRequestComparer
    {
        private readonly IRandomProvider _randomProvider;

        public DefaultRequestComparer(IRandomProvider randomProvider)
        {
            _randomProvider = randomProvider;
        }


        public Comparison<BattleControllerRequest> GetComparer(BattleInstance battleInstance)
        {
            return (x, y) => Compare(battleInstance, x, y);
        }


        private int CompareSpeed(BattleInstance battleInstance, BattleControllerRequest x, BattleControllerRequest y)
        {
            SpeciesBattleInstance xInstance = battleInstance.Combatants[x.Side, x.Slot];
            SpeciesBattleInstance yInstance = battleInstance.Combatants[y.Side, y.Slot];

            int speedComparison = xInstance.CurrentSpeed.CompareTo(yInstance.CurrentSpeed);

            if (speedComparison != 0)
                return speedComparison;

            return _randomProvider.Next(2) == 0 ? -1 : 1;
        }

        protected int Compare(BattleInstance battleInstance, BattleControllerRequest x, BattleControllerRequest y)
        {
            int xPriority = (int)x.InputType;
            int yPriority = (int)y.InputType;

            return xPriority != yPriority ? xPriority.CompareTo(yPriority) : CompareSpeed(battleInstance, x, y);
        }
    }
}