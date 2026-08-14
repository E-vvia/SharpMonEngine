using System.Collections.Generic;
using SharpMonEngine.Battle.Core.Interfaces.Actions;
using SharpMonEngine.Battle.Core.Model;
using SharpMonEngine.Core.Interfaces.Providers;

namespace SharpMonEngine.Actions
{
    public class DefaultActionSorter : IActionSorter
    {
        public IComparer<IPlayerAction> GetComparer(BattleInstance battleInstance, IRandomProvider randomProvider)
        {
            return new PlayerActionComparer(battleInstance, randomProvider);
        }

        private class PlayerActionComparer : IComparer<IPlayerAction>
        {
            private readonly BattleInstance _battleInstance;
            private readonly IRandomProvider _randomProvider;

            public PlayerActionComparer(BattleInstance battleInstance, IRandomProvider randomProvider)
            {
                _battleInstance = battleInstance;
                _randomProvider = randomProvider;
            }

            public int Compare(IPlayerAction x, IPlayerAction y)
            {
                int xPriority = (int)x.ActionType;
                int yPriority = (int)y.ActionType;

                return xPriority != yPriority ? xPriority.CompareTo(yPriority) : CompareSpeed(_battleInstance, x, y);
            }

            private int CompareSpeed(BattleInstance battleInstance, IPlayerAction x, IPlayerAction y)
            {
                SpeciesBattleInstance xInstance = battleInstance.Sides[x.Side]
                    .Slots[x.Slot]
                    .SpeciesBattleInstance!;
                SpeciesBattleInstance yInstance = battleInstance.Sides[x.Side]
                    .Slots[x.Slot]
                    .SpeciesBattleInstance!;

                int speedComparison = xInstance.CurrentSpeed.CompareTo(yInstance.CurrentSpeed);

                if (speedComparison != 0)
                    return speedComparison;

                return _randomProvider.Next(2) == 0 ? -1 : 1;
            }
        }
    }
}