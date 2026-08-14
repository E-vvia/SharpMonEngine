using SharpMonEngine.Battle.Core.Model;

namespace SharpMonEngine.Battle.Core.Interfaces.Builder
{
    public interface IBattleInstanceBuilder
    {
        IBattleInstanceBuilder SetSides(int sides);
        IBattleInstanceBuilder SetSlotsPerSide(int slotsPerSide);
        IBattleInstanceBuilder SetSideAvailableCombatant(int side, int availablePokemon);
        BattleInstance Build();
        IBattleInstanceBuilder Clear();
        IBattleInstanceBuilder SetWild();
    }
}