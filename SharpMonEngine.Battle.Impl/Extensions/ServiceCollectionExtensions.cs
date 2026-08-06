using Microsoft.Extensions.DependencyInjection;
using SharpMonEngine.Battle.Core.Interfaces.Comparers;
using SharpMonEngine.Battle.Core.Interfaces.Controller;
using SharpMonEngine.Battle.Core.Interfaces.Providers;
using SharpMonEngine.Battle.Core.Interfaces.Services;
using SharpMonEngine.Battle.Core.Model.Data;
using SharpMonEngine.Comparers;
using SharpMonEngine.Controller;
using SharpMonEngine.Core.Interfaces.Providers;
using SharpmonEngine.Impl.Providers;
using SharpMonEngine.Services;

namespace SharpMonEngine.Battle.Impl.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBattleSystem(this IServiceCollection services)
        {
            services.AddScoped<IBattleControllerRequestComparer, DefaultRequestComparer>();
            services.AddScoped<IRandomProvider, RandomProvider>();
            services.AddScoped<IDamageCalculationService, DamageCalculationService>();
            services.AddScoped<IBattleController, BattleController>();
            services.AddSingleton<IDataProvider, DataProvider>();
            services.AddSingleton<ICalculationModifierProvider>();
            return services;
        }

        public static IServiceCollection AddDefaultCalculationModifiers(this IServiceCollection services,
            CalculationModifierData calculationModifiers)
        {
            return services;
        }
    }
}