using System;
using System.Collections.Generic;
using System.Linq;
using SharpMonEngine.Battle.Core.Interfaces.Modifiers;
using SharpMonEngine.Battle.Core.Interfaces.Providers;
using SharpMonEngine.Core.Model;
using SharpMonEngine.Model.Providers;
using SharpMonEngine.Modifiers;

namespace SharpMonEngine.Providers
{
    public class CalculationModifierProvider : ICalculationModifierProvider
    {
        internal CalculationModifierCollection CalculationModifiers { get; }

        public CalculationModifierProvider(CalculationModifierCollection calculationModifiers)
        {
            CalculationModifiers = calculationModifiers;
        }

        public ICalculationModifier GetMoveCalculationModifier(int moveId)
        {
            return GetModifierOrDefault(moveId, CalculationModifiers.MoveCalculationModifiers,
                () => new DefaultCalculationModifier());
        }

        public IEnumerable<ICalculationModifier> GetFlagsCalculationModifiers(byte flags)
        {
            return CalculationModifiers.FlagCalculationModifiers.Where(pair => (flags & (uint)pair.Key) != 0)
                .Select(pair => pair.Value)
                .Append(new DefaultFlagCalculationModifier());
        }

        public ICalculationModifier GetWeatherCalculationModifier(byte weatherId)
        {
            return GetModifierOrDefault(weatherId, CalculationModifiers.WeatherCalculationModifiers,
                () => new DefaultWeatherCalculationModifier());
        }

        public ICalculationModifier GetAbilityCriticalHitCalculationModifier(int targetAbilityId)
        {
            return GetModifierOrDefault(targetAbilityId, CalculationModifiers.AbilityCriticalHitCalculationModifiers,
                () => new DefaultCriticalHitCalculationModifier());
        }

        public ICalculationModifier GetStatusCriticalHitCalculationModifier(byte status)
        {
            return GetModifierOrDefault(status, CalculationModifiers.StatusCriticalHitCalculationModifiers,
                () => new DefaultCriticalHitCalculationModifier());
        }

        public ICalculationModifier GetMoveCriticalHitCalculationModifier(int moveId)
        {
            return GetModifierOrDefault(moveId, CalculationModifiers.MoveCriticalHitCalculationModifiers,
                () => new DefaultCriticalHitCalculationModifier());
        }

        public ICalculationModifier GetStabCalculationModifier(int attackerAbilityId)
        {
            return GetModifierOrDefault(attackerAbilityId, CalculationModifiers.StabCalculationModifiers,
                () => new DefaultStabCalculationModifier());
        }

        public ICalculationModifier GetTypeCalculationModifier(int moveId)
        {
            return GetModifierOrDefault(moveId, CalculationModifiers.TypeCalculationModifiers,
                () => new DefaultTypeCalculationModifier());
        }


        private T GetModifierOrDefault<T>(int id, Dictionary<int, T> modifiers, Func<T> defaultValue)
        {
            return modifiers.TryGetValue(id, out T modify) ? modify : defaultValue();
        }
    }
}