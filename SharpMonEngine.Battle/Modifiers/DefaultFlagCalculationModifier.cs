using SharpMonEngine.Battle.Core.Context.Services;
using SharpMonEngine.Battle.Core.Interfaces.Modifiers;

namespace SharpMonEngine.Modifiers
{
    public class DefaultFlagCalculationModifier : ICalculationModifier
    {
        public float GetModifier(DamageCalculationContext damageCalculationContext)
        {
            return 1;
        }
    }
}