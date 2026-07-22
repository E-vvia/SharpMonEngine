using SharpMonEngine.Battle.Core.Context.Services;
using SharpMonEngine.Battle.Core.Interfaces.Modifiers;

namespace SharpMonEngine.Modifiers
{
    public class DefaultCalculationModifier : ICalculationModifier
    {
        public float GetModifier(DamageCalculationContext damageCalculationContext)
        {
            return damageCalculationContext.UsedMove.Power;
        }
    }
}