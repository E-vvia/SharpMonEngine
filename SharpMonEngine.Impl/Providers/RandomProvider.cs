using System;
using SharpMonEngine.Core.Interfaces.Providers;

namespace SharpmonEngine.Impl.Providers
{
    public class RandomProvider : IRandomProvider
    {
        private readonly Random _random;

        public RandomProvider()
        {
            _random = new Random();
        }

        public int Next()
        {
            return _random.Next();
        }

        public int Next(int maxValue)
        {
            return _random.Next(maxValue);
        }

        public int Next(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }
    }
}