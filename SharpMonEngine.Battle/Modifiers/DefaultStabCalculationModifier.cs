using SharpMonEngine.Battle.Core.Context.Services;
using SharpMonEngine.Battle.Core.Interfaces.Modifiers;

namespace SharpMonEngine.Modifiers
{
    public class DefaultStabCalculationModifier : ICalculationModifier
    {
        public float GetModifier(DamageCalculationContext damageCalculationContext)
        {
            if (damageCalculationContext.Attacker.Type1 == damageCalculationContext.UsedMove.Type ||
                damageCalculationContext.Attacker.Type2 == damageCalculationContext.UsedMove.Type)
            {
                return 1.5f;
            }
            else
            {
                return 1;
            }
        }
    }
}