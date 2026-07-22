using System;
using System.Collections.Generic;
using SharpMonEngine.Battle.Core.Interfaces.Modifiers;
using SharpMonEngine.Battle.Core.Model;
using SharpMonEngine.Core.Model;

namespace SharpMonEngine.Battle.Core.Interfaces.Providers
{
    public interface ICalculationModifierProvider
    {
        ICalculationModifier GetMoveCalculationModifier(int moveId);
        IEnumerable<ICalculationModifier> GetFlagsCalculationModifiers(byte flags);
        ICalculationModifier GetWeatherCalculationModifier(byte weatherId);
        ICalculationModifier GetAbilityCriticalHitCalculationModifier(int targetAbilityId);
        ICalculationModifier GetStatusCriticalHitCalculationModifier(byte status);
        ICalculationModifier GetMoveCriticalHitCalculationModifier(int moveId);
        ICalculationModifier GetStabCalculationModifier(int attackerAbilityId);
        ICalculationModifier GetTypeCalculationModifier(int moveId);
    }
}