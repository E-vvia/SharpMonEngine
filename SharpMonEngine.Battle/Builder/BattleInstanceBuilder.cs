using System;
using SharpMonEngine.Battle.Core.Interfaces.Builder;
using SharpMonEngine.Battle.Core.Model;

namespace SharpMonEngine.Builder
{
    public sealed class BattleInstanceBuilder : IBattleInstanceBuilder
    {
        private BattleInstance? _battleInstance;

        private BattleInstance Battle =>
            _battleInstance ??= new BattleInstance();

        public IBattleInstanceBuilder SetSides(int sides)
        {
            if (sides < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(sides), "A battle must have at least two sides.");
            }

            Battle.SidesNumber = sides;
            Battle.Sides = new Side[sides];

            for (int sideId = 0; sideId < sides; sideId++)
            {
                Battle.Sides[sideId] = new Side
                {
                    Id = sideId,
                    BattleInstance = Battle
                };
            }

            return this;
        }

        public IBattleInstanceBuilder SetSlotsPerSide(int slotsPerSide)
        {
            if (slotsPerSide < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(slotsPerSide), "A side must have at least one slot.");
            }

            Battle.SlotsPerSideNumber = slotsPerSide;

            foreach (Side side in Battle.Sides)
            {
                side.Slots = new Slot[slotsPerSide];

                for (int slotId = 0; slotId < slotsPerSide; slotId++)
                {
                    side.Slots[slotId] = new Slot
                    {
                        Id = slotId,
                        Side = side
                    };
                }
            }

            return this;
        }

        public IBattleInstanceBuilder SetSideAvailableCombatant(int side, int availablePokemon)
        {
            ValidateSideIndex(side);

            if (availablePokemon < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(availablePokemon),
                    "Available Pokémon must be at least 1.");
            }

            Battle.Sides[side].AvailablePokemonNumber = availablePokemon;

            return this;
        }

        public IBattleInstanceBuilder SetWild()
        {
            Battle.IsWild = true;
            return this;
        }

        public IBattleInstanceBuilder Clear()
        {
            _battleInstance = null;
            return this;
        }

        public BattleInstance Build()
        {
            Validate();
            BattleInstance battle = Battle;
            _battleInstance = null;
            return battle;
        }

        private void Validate()
        {
            if (Battle.SidesNumber <= 0)
            {
                throw new InvalidOperationException("The battle must have at least one side.");
            }

            if (Battle.SlotsPerSideNumber <= 0)
            {
                throw new InvalidOperationException("The battle must have at least one slot per side.");
            }

            if (Battle.Sides.Length != Battle.SidesNumber)
            {
                throw new InvalidOperationException("The number of sides does not match the sides collection.");
            }

            foreach (Side side in Battle.Sides)
            {
                if (side == null)
                {
                    throw new InvalidOperationException("The battle contains an uninitialized side.");
                }

                if (side.Slots.Length != Battle.SlotsPerSideNumber)
                {
                    throw new InvalidOperationException("A side does not contain the expected number of slots.");
                }
            }
        }

        private void ValidateSideIndex(int side)
        {
            if (side < 0 || side >= Battle.Sides.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(side), side, "The specified side does not exist.");
            }
        }
    }
}