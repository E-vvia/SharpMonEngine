using System.Collections.Generic;
using SharpMonEngine.Battle.Core.Model;
using SharpMonEngine.Core.Interfaces.Providers;

namespace SharpMonEngine.Battle.Core.Interfaces.Actions
{
    public interface IActionSorter
    {
        IComparer<IPlayerAction> GetComparer(BattleInstance battleInstance, IRandomProvider randomProvider);
    }
}