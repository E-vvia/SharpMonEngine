using System;
using Microsoft.Extensions.DependencyInjection;
using SharpMonEngine.Battle.Core.Interfaces.Actions;
using SharpMonEngine.Battle.Core.Interfaces.Builder;
using SharpMonEngine.Battle.Core.Model;
using SharpMonEngine.Battle.Impl.Configuration;
using SharpMonEngine.Battle.Impl.Extensions;
using SharpMonEngine.Core.Interfaces.Providers;
using SharpMonEngine.Session;

namespace SharpMonEngine.Battle.Impl
{
    public sealed class SharpMonBattleEngine : IDisposable
    {
        private bool _disposed;
        private ServiceProvider? _services;

        public SharpMonBattleEngine()
        {
            ServiceCollection services = new ServiceCollection();

            services.AddBattleSystem("data");

            _services = services.BuildServiceProvider();
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _services?.Dispose();
            _services = null;
            _disposed = true;
        }


        public BattleSession CreateWildBattle(BattleConfiguration configuration)
        {
            ThrowIfDisposed();

            using IServiceScope scope = _services!.CreateScope();

            IServiceProvider provider = scope.ServiceProvider;

            IActionSorter actionSorter =
                provider.GetRequiredService<IActionSorter>();

            IRandomProvider randomProvider =
                provider.GetRequiredService<IRandomProvider>();

            IBattleInstanceBuilder builder =
                provider.GetRequiredService<IBattleInstanceBuilder>();

            BattleInstance battle = builder
                .SetWild()
                .SetSides(configuration.Sides)
                .SetSlotsPerSide(configuration.SlotsPerSide)
                .Build();

            return new BattleSession(battle, actionSorter, randomProvider);
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(SharpMonBattleEngine));
            }
        }
    }
}