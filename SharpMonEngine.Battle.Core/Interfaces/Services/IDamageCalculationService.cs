using SharpMonEngine.Battle.Core.Context.Services;

namespace SharpMonEngine.Battle.Core.Interfaces.Services
{
    public interface IDamageCalculationService
    {
        double CalculateDamage(DamageCalculationContext damageCalculationContext);
    }
}