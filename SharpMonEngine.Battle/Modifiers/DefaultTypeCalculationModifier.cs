using SharpMonEngine.Battle.Core.Context.Services;
using SharpMonEngine.Battle.Core.Interfaces.Modifiers;
using SharpMonEngine.Model.Data;

namespace SharpMonEngine.Modifiers
{
    public class DefaultTypeCalculationModifier : ICalculationModifier
    {
        public float GetModifier(DamageCalculationContext damageCalculationContext)
        {
            return TypeChart.GetEffectiveness(damageCalculationContext.UsedMove.Type,
                damageCalculationContext.Defender.Type1, damageCalculationContext.Defender.Type2);
        }
    }
}