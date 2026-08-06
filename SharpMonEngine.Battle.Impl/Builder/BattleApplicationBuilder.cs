using System;
using Microsoft.Extensions.DependencyInjection;
using SharpMonEngine.Battle.Impl.Extensions;

namespace SharpMonEngine.Battle.Impl.Builder
{
    public class BattleApplicationBuilder
    {
        public BattleApplicationBuilder()
        {
            Services.AddBattleSystem();
        }

        public IServiceCollection Services { get; } = new ServiceCollection();

        public IServiceProvider Build()
        {
            return Services.BuildServiceProvider();
        }
    }
}