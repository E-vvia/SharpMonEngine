using System;
using System.Collections.Generic;
using SharpMonEngine.Battle.Core.Interfaces.Events;

namespace SharpMonEngine.Battle.Core.Model
{
    public class BattleInstance
    {
        private List<IBattleEvent> _battleEvents = new List<IBattleEvent>();
        public int SidesNumber { get; set; }
        public int SlotsPerSideNumber { get; set; }
        public Side[] Sides { get; set; } = Array.Empty<Side>();
        public int TurnNumber { get; set; } = 0;
        public bool IsWild { get; set; } = false;

        public IReadOnlyCollection<IBattleEvent> BattleEvents => _battleEvents.AsReadOnly();

        public void DoForEachSlot(Action<Slot> action)
        {
            foreach (Side side in Sides)
            {
                foreach (Slot slot in side.Slots)
                {
                    action(slot);
                }
            }
        }

        public void AddBattleEvent(IBattleEvent battleEvent)
        {
            _battleEvents.Add(battleEvent);
        }

        public void SetCombatant(SpeciesBattleInstance instance, int side, int slot)
        {
            Sides[side].SetCombatant(instance, slot);
        }
    }
}