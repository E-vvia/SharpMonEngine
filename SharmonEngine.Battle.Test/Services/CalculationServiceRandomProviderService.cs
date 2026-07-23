using SharpMonEngine.Core.Interfaces.Services;

namespace SharmonEngine.Battle.Test.Services
{
    public class CalculationServiceRandomProviderService : IRandomProviderService
    {
        private int _index = -1;
        
        public int Next()
        {
            return _index++;
        }

        public int Next(int maxValue)
        {
            return _index++;
        }

        public int Next(int minValue, int maxValue)
        {
            return minValue + _index++;
        }
    }
}