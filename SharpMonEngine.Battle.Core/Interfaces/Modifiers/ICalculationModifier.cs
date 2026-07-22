using SharpMonEngine.Battle.Core.Context.Services;

namespace SharpMonEngine.Battle.Core.Interfaces.Modifiers
{
    public interface ICalculationModifier
    {
        float GetModifier(DamageCalculationContext damageCalculationContext);
    }
}