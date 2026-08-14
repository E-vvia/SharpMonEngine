using Microsoft.Extensions.DependencyInjection;
using SharpMonEngine.Battle.Core.Interfaces.Builder;
using SharpMonEngine.Battle.Core.Interfaces.Providers;
using SharpMonEngine.Battle.Core.Interfaces.Services;
using SharpMonEngine.Battle.Core.Model.Data;
using SharpMonEngine.Builder;
using SharpMonEngine.Core.Interfaces.Providers;
using SharpmonEngine.Impl.Providers;
using SharpMonEngine.Providers;
using SharpMonEngine.Services;

namespace SharpMonEngine.Battle.Impl.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBattleSystem(this IServiceCollection services, string dataFolderPath)
        {
            services.AddSingleton<IRandomProvider, RandomProvider>();
            services.AddSingleton<IDataProvider, DataProvider>();
            services.AddSingleton<ICalculationModifierProvider, CalculationModifierProvider>();
            services.AddScoped<IDamageCalculationService, DamageCalculationService>();
            services.AddTransient<IBattleInstanceBuilder, BattleInstanceBuilder>();
            return services;
        }

        public static IServiceCollection AddDefaultCalculationModifiers(this IServiceCollection services,
            CalculationModifierData calculationModifiers)
        {
            return services;
        }
    }
}