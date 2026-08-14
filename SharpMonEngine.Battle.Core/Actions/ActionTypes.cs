using System;

namespace SharpMonEngine.Battle.Core.Actions
{
    [Flags]
    public enum PlayerActionType
    {
        None,
        SendCombatant,
        UseMove,
        UseItem,
        SwapCombatant,
        Run
    }
}