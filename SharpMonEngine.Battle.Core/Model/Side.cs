using System;

namespace SharpMonEngine.Battle.Core.Model
{
    public class Side
    {
        public BattleInstance BattleInstance { get; set; } = null!;
        public int Id { get; set; }
        public int AvailablePokemonNumber { get; set; }
        public Slot[] Slots { get; set; } = Array.Empty<Slot>();

        public void SetCombatant(SpeciesBattleInstance instance, int slot)
        {
            Slots[slot].SetCombatant(instance);
        }
    }
}