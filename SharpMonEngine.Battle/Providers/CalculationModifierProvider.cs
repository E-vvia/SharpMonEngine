using System;
using System.Collections.Generic;
using System.Linq;
using SharpMonEngine.Battle.Core.Interfaces.Modifiers;
using SharpMonEngine.Battle.Core.Interfaces.Providers;
using SharpMonEngine.Battle.Core.Model.Data;
using SharpMonEngine.Modifiers;

namespace SharpMonEngine.Providers
{
    public class CalculationModifierProvider : ICalculationModifierProvider
    {
        private CalculationModifierData _calculationModifiers;

        public CalculationModifierProvider()
        {
            _calculationModifiers = new CalculationModifierData();
        }

        public ICalculationModifier GetMoveCalculationModifier(int moveId)
        {
            return GetModifierOrDefault(moveId, _calculationModifiers.MoveCalculationModifiers,
                () => new DefaultCalculationModifier());
        }

        public IEnumerable<ICalculationModifier> GetFlagsCalculationModifiers(byte flags)
        {
            return _calculationModifiers.FlagCalculationModifiers.Where(pair => (flags & (uint)pair.Key) != 0)
                .Select(pair => pair.Value)
                .Append(new DefaultFlagCalculationModifier());
        }

        public ICalculationModifier GetWeatherCalculationModifier(byte weatherId)
        {
            return GetModifierOrDefault(weatherId, _calculationModifiers.WeatherCalculationModifiers,
                () => new DefaultWeatherCalculationModifier());
        }

        public ICalculationModifier GetAbilityCriticalHitCalculationModifier(int targetAbilityId)
        {
            return GetModifierOrDefault(targetAbilityId, _calculationModifiers.AbilityCriticalHitCalculationModifiers,
                () => new DefaultCriticalHitCalculationModifier());
        }

        public ICalculationModifier GetStatusCriticalHitCalculationModifier(byte status)
        {
            return GetModifierOrDefault(status, _calculationModifiers.StatusCriticalHitCalculationModifiers,
                () => new DefaultCriticalHitCalculationModifier());
        }

        public ICalculationModifier GetMoveCriticalHitCalculationModifier(int moveId)
        {
            return GetModifierOrDefault(moveId, _calculationModifiers.MoveCriticalHitCalculationModifiers,
                () => new DefaultCriticalHitCalculationModifier());
        }

        public ICalculationModifier GetStabCalculationModifier(int attackerAbilityId)
        {
            return GetModifierOrDefault(attackerAbilityId, _calculationModifiers.StabCalculationModifiers,
                () => new DefaultStabCalculationModifier());
        }

        public ICalculationModifier GetTypeCalculationModifier(int moveId)
        {
            return GetModifierOrDefault(moveId, _calculationModifiers.TypeCalculationModifiers,
                () => new DefaultTypeCalculationModifier());
        }


        private T GetModifierOrDefault<T>(int id, Dictionary<int, T> modifiers, Func<T> defaultValue)
        {
            return modifiers.TryGetValue(id, out T modify) ? modify : defaultValue();
        }
    }
}