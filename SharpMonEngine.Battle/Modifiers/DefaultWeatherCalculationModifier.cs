using SharpMonEngine.Battle.Core.Context.Services;
using SharpMonEngine.Battle.Core.Interfaces.Modifiers;

namespace SharpMonEngine.Modifiers
{
    public class DefaultWeatherCalculationModifier : ICalculationModifier
    {
        public float GetModifier(DamageCalculationContext damageCalculationContext)
        {
            return 1;
        }
    }
}