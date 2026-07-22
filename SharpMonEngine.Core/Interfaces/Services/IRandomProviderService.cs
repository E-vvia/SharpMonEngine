namespace SharpMonEngine.Core.Interfaces.Services
{
    public interface IRandomProviderService
    {
        int Next();
        int Next(int maxValue);
        int Next(int minValue, int maxValue);
    }
}