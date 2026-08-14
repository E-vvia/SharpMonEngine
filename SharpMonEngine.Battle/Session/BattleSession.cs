using System.Collections.Generic;
using System.Linq;
using SharpMonEngine.Battle.Core.Actions;
using SharpMonEngine.Battle.Core.Interfaces.Actions;
using SharpMonEngine.Battle.Core.Model;
using SharpMonEngine.Core.Interfaces.Providers;

namespace SharpMonEngine.Session
{
    public class BattleSession
    {
        private readonly IActionSorter _actionSorter;
        private readonly BattleInstance _battle;
        private readonly IRandomProvider _randomProvider;

        public BattleSession(BattleInstance battle, IActionSorter actionSorter, IRandomProvider randomProvider)
        {
            _battle = battle;
            _actionSorter = actionSorter;
            _randomProvider = randomProvider;
        }

        public void Setup()
        {
            SetRequestedActionForEmptySlots(PlayerActionType.SendCombatant);
        }

        public bool Start(IEnumerable<SendPlayerAction> actions)
        {
            if (!CanStart())
            {
                return false;
            }

            ResolveActions(actions);

            return true;
        }

        public bool PrepareTurn()
        {
            if (!AllSlotsOccupied())
            {
                SetRequestedActionForEmptySlots(PlayerActionType.SendCombatant);
                return true;
            }

            _battle.TurnNumber++;

            _battle.DoForEachSlot(slot =>
            {
                slot.RequestedPlayerActionType =
                    PlayerActionType.UseMove |
                    PlayerActionType.UseItem |
                    PlayerActionType.SwapCombatant |
                    PlayerActionType.Run;
            });

            return true;
        }

        public bool ResolveTurn(IList<IPlayerAction> actions)
        {
            if (!CanResolveTurn(actions))
            {
                return false;
            }

            ResolveActions(actions);

            return true;
        }

        private bool CanStart()
        {
            return _battle.TurnNumber == 0;
        }

        private bool CanResolveTurn(IEnumerable<IPlayerAction> actions)
        {
            foreach (var action in actions)
            {
                var slot = GetSlot(action);

                if (!slot.IsValidAction(action.ActionType))
                {
                    return false;
                }
            }

            return true;
        }

        private bool AllSlotsOccupied()
        {
            var allOccupied = true;

            _battle.DoForEachSlot(slot =>
            {
                if (slot.SpeciesBattleInstance == null)
                {
                    allOccupied = false;
                }
            });

            return allOccupied;
        }

        private void SetRequestedActionForEmptySlots(PlayerActionType actionType)
        {
            _battle.DoForEachSlot(slot =>
            {
                if (slot.SpeciesBattleInstance == null)
                {
                    slot.RequestedPlayerActionType = actionType;
                }
            });
        }

        private void ResolveActions<TAction>(IEnumerable<TAction> actions)
            where TAction : IPlayerAction
        {
            var comparer = _actionSorter.GetComparer(_battle, _randomProvider);

            foreach (TAction action in actions.OrderBy(action => action, comparer))
            {
                action.Resolve(_battle);
            }
        }

        private Slot GetSlot(IPlayerAction action)
        {
            return _battle.Sides[action.Side].Slots[action.Slot];
        }
    }
}