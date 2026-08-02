namespace SharpMonEngine.Core.Interfaces.Providers
{
    public interface IRandomProvider
    {
        int Next();
        int Next(int maxValue);
        int Next(int minValue, int maxValue);
    }
}