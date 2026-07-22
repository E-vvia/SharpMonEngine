using System;
using System.Collections.Generic;
using System.Linq;
using SharpMonEngine.Battle.Core.Context.Services;
using SharpMonEngine.Battle.Core.Interfaces.Modifiers;
using SharpMonEngine.Battle.Core.Interfaces.Providers;
using SharpMonEngine.Battle.Core.Interfaces.Services;
using SharpMonEngine.Core.Interfaces.Services;

namespace SharpMonEngine.Services
{
    public class DamageCalculationService : IDamageCalculationService
    {
        private readonly ICalculationModifierProvider _calculationModifierProvider;
        private readonly IRandomProviderService _randomProviderService;

        public DamageCalculationService(ICalculationModifierProvider calculationModifierProvider,
            IRandomProviderService randomProviderService)
        {
            _calculationModifierProvider = calculationModifierProvider;
            _randomProviderService = randomProviderService;
        }

        public double CalculateDamage(DamageCalculationContext damageCalculationContext)
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

            double damage = 1;
            double baseDamage = CalculateBaseDamage(damageCalculationContext, calculationModifier);
            double targets = damageCalculationContext.Multitarget ? 0.75 : 1;
            double weather = stabCalculationModifier.GetModifier(damageCalculationContext);
            double flags = flagsCalculationModifiers.Aggregate(1.0,
                (total, modifier) => total * modifier.GetModifier(damageCalculationContext));

            double criticalHit = GetCriticalHitModifier(damageCalculationContext);
            double random = _randomProviderService.Next(85, 101) / 100.0f;
            double stab = stabCalculationModifier.GetModifier(damageCalculationContext);
            double type = typeCalculationModifier.GetModifier(damageCalculationContext);


            return damage * baseDamage * targets * weather * flags * criticalHit * random * stab * type;
        }

        private double CalculateBaseDamage(DamageCalculationContext damageCalculationContext,
            ICalculationModifier calculationModifier)
        {
            double damage =
                ((2.0 * damageCalculationContext.Attacker.Level / 5 + 2) *
                    calculationModifier.GetModifier(damageCalculationContext) *
                    damageCalculationContext.Attacker.CurrentAtk /
                    damageCalculationContext.Defender.CurrentDef / 50 + 2);

            return damage;
        }

        private bool IsCriticalHit()
        {
            int rValue = _randomProviderService.Next(0, 100);
            return rValue >= 50;
        }

        private double GetCriticalHitModifier(DamageCalculationContext damageCalculationContext)
        {
            damageCalculationContext.WasCriticalHit = IsCriticalHit();
            ICalculationModifier targetAbilityCritHitModifier =
                _calculationModifierProvider.GetAbilityCriticalHitCalculationModifier(damageCalculationContext.Defender
                    .AbilityId);

            ICalculationModifier targetStatusCritHitModifier =
                _calculationModifierProvider.GetAbilityCriticalHitCalculationModifier(damageCalculationContext.Defender
                    .Status);


            ICalculationModifier moveCriticalHitModifier =
                _calculationModifierProvider.GetMoveCriticalHitCalculationModifier(damageCalculationContext.UsedMove
                    .Id);


            ICalculationModifier userAbilityCritHitModifier =
                _calculationModifierProvider.GetAbilityCriticalHitCalculationModifier(damageCalculationContext.Attacker
                    .AbilityId);

            ICalculationModifier userStatusCritHitModifier =
                _calculationModifierProvider.GetAbilityCriticalHitCalculationModifier(damageCalculationContext.Attacker
                    .Status);


            double targetAbilityCritModifierValue =
                targetAbilityCritHitModifier.GetModifier(damageCalculationContext);

            if (damageCalculationContext.WasCriticalHit && targetAbilityCritModifierValue < 1.5f)
            {
                return targetAbilityCritModifierValue;
            }

            double targetStatusAbilityCritModifierValue =
                targetStatusCritHitModifier.GetModifier(damageCalculationContext);

            if (damageCalculationContext.WasCriticalHit && targetStatusAbilityCritModifierValue < 1.5f)
            {
                return targetStatusAbilityCritModifierValue;
            }

            double moveCriticalHitModifierValue =
                moveCriticalHitModifier.GetModifier(damageCalculationContext);

            if (damageCalculationContext.WasCriticalHit && moveCriticalHitModifierValue > 1)
            {
                return moveCriticalHitModifierValue;
            }

            double userAbilityCriticalHitModifierValue =
                userAbilityCritHitModifier.GetModifier(damageCalculationContext);

            if (damageCalculationContext.WasCriticalHit && userAbilityCriticalHitModifierValue > 1)
            {
                return userAbilityCriticalHitModifierValue;
            }

            double userStatusCriticalHitModifierValue =
                userStatusCritHitModifier.GetModifier(damageCalculationContext);

            if (damageCalculationContext.WasCriticalHit && userStatusCriticalHitModifierValue > 1)
            {
                return userStatusCriticalHitModifierValue;
            }

            return damageCalculationContext.WasCriticalHit ? 1.5f : 1;
        }
    }
}