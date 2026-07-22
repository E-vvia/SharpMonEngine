using SharpMonEngine.Battle.Core.Context.Services;
using SharpMonEngine.Battle.Core.Interfaces.Modifiers;

namespace SharpMonEngine.Modifiers
{
    public class DefaultCriticalHitCalculationModifier : ICalculationModifier
    {
        public DefaultCriticalHitCalculationModifier()
        {
        }

        public float GetModifier(DamageCalculationContext damageCalculationContext)
        {
            return 1;
        }
    }
}