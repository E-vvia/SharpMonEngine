using System;
using System.Collections.Generic;
using System.Linq;
using SharpMonEngine.Battle.Core.Context.Services;
using SharpMonEngine.Battle.Core.Interfaces.Modifiers;
using SharpMonEngine.Battle.Core.Interfaces.Providers;
using SharpMonEngine.Battle.Core.Interfaces.Services;
using SharpMonEngine.Core.Interfaces.Providers;

namespace SharpMonEngine.Services
{
    public class DamageCalculationService : IDamageCalculationService
    {
        private readonly ICalculationModifierProvider _calculationModifierProvider;
        private readonly IRandomProvider _randomProvider;

        public DamageCalculationService(ICalculationModifierProvider calculationModifierProvider,
            IRandomProvider randomProvider)
        {
            _calculationModifierProvider = calculationModifierProvider;
            _randomProvider = randomProvider;
        }

        public int CalculateDamage(DamageCalculationContext damageCalculationContext)
        {
            ICalculationModifier calculationModifier =
                _calculationModifierProvider.GetMoveCalculationModifier(damageCalculationContext.UsedMove.Id);

            ICalculationModifier weatherCalculationModifier =
                _calculationModifierProvider.GetWeatherCalculationModifier(damageCalculationContext.ActiveWeather);

            IEnumerable<ICalculationModifier> flagsCalculationModifiers =
                _calculationModifierProvider.GetFlagsCalculationModifiers(damageCalculationContext.Flags);

            ICalculationModifier stabCalculationModifier =
                _calculationModifierProvider.GetStabCalculationModifier(damageCalculationContext.Attacker.AbilityId);

            ICalculationModifier typeCalculationModifier =
                _calculationModifierProvider.GetTypeCalculationModifier(damageCalculationContext.UsedMove.Id);

            float damage = 1;
            float baseDamage = CalculateBaseDamage(damageCalculationContext, calculationModifier);
            float targets = damageCalculationContext.Multitarget ? 0.75f : 1;
            float weather = weatherCalculationModifier.GetModifier(damageCalculationContext);
            float flags = flagsCalculationModifiers.Aggregate(1.0f,
                (total, modifier) => total * modifier.GetModifier(damageCalculationContext));

            float criticalHit = GetCriticalHitModifier(damageCalculationContext);
            float random = (float)(MathF.Truncate(GetRandom() * 100) / 100.0f);
            float stab = stabCalculationModifier.GetModifier(damageCalculationContext);
            float type = typeCalculationModifier.GetModifier(damageCalculationContext);

            float totalDamage =
                damage *
                RoundHalfUp(baseDamage) *
                targets *
                weather *
                flags *
                criticalHit *
                random *
                stab *
                type;
            return (int)totalDamage;
        }

        private float GetRandom()
        {
            return (_randomProvider.Next(85, 101) / 100.0f);
        }

        private float CalculateBaseDamage(DamageCalculationContext damageCalculationContext,
            ICalculationModifier calculationModifier)
        {
            float damage =
                ((((2.0f * damageCalculationContext.Attacker.Level / 5) + 2) *
                  calculationModifier.GetModifier(damageCalculationContext) *
                  damageCalculationContext.Attacker.CurrentAtk /
                  damageCalculationContext.Defender.CurrentDef) / 50) + 2;
            return damage;
        }

        private bool IsCriticalHit()
        {
            int rValue = _randomProvider.Next(2);
            return rValue == 0;
        }

        private float GetCriticalHitModifier(DamageCalculationContext damageCalculationContext)
        {
            damageCalculationContext.WasCriticalHit = IsCriticalHit();
            ICalculationModifier targetAbilityCritHitModifier =
                _calculationModifierProvider.GetAbilityCriticalHitCalculationModifier(damageCalculationContext.Defender
                    .AbilityId);

            ICalculationModifier targetStatusCritHitModifier =
                _calculationModifierProvider.GetStatusCriticalHitCalculationModifier(damageCalculationContext.Defender
                    .Status);


            ICalculationModifier moveCriticalHitModifier =
                _calculationModifierProvider.GetMoveCriticalHitCalculationModifier(damageCalculationContext.UsedMove
                    .Id);


            ICalculationModifier userAbilityCritHitModifier =
                _calculationModifierProvider.GetAbilityCriticalHitCalculationModifier(damageCalculationContext.Attacker
                    .AbilityId);

            ICalculationModifier userStatusCritHitModifier =
                _calculationModifierProvider.GetStatusCriticalHitCalculationModifier(damageCalculationContext.Attacker
                    .Status);


            float targetAbilityCritModifierValue =
                targetAbilityCritHitModifier.GetModifier(damageCalculationContext);

            if (damageCalculationContext.WasCriticalHit && targetAbilityCritModifierValue < 1.5f)
            {
                return targetAbilityCritModifierValue;
            }

            float targetStatusAbilityCritModifierValue =
                targetStatusCritHitModifier.GetModifier(damageCalculationContext);

            if (damageCalculationContext.WasCriticalHit && targetStatusAbilityCritModifierValue < 1.5f)
            {
                return targetStatusAbilityCritModifierValue;
            }

            float moveCriticalHitModifierValue =
                moveCriticalHitModifier.GetModifier(damageCalculationContext);

            if (damageCalculationContext.WasCriticalHit && moveCriticalHitModifierValue > 1)
            {
                return moveCriticalHitModifierValue;
            }

            float userAbilityCriticalHitModifierValue =
                userAbilityCritHitModifier.GetModifier(damageCalculationContext);

            if (damageCalculationContext.WasCriticalHit && userAbilityCriticalHitModifierValue > 1)
            {
                return userAbilityCriticalHitModifierValue;
            }

            float userStatusCriticalHitModifierValue =
                userStatusCritHitModifier.GetModifier(damageCalculationContext);

            if (damageCalculationContext.WasCriticalHit && userStatusCriticalHitModifierValue > 1)
            {
                return userStatusCriticalHitModifierValue;
            }

            return damageCalculationContext.WasCriticalHit ? 1.5f : 1;
        }

        public static int RoundHalfUp(float value)
        {
            return (int)MathF.Floor(value + 0.5f);
        }
    }
}