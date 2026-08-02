using SharpMonEngine.Core.Interfaces.Model;

namespace SharpMonEngine.Core.Interfaces.Providers
{
    public interface IDataProvider
    {
        IMoveData GetMoveData(int id);
    }
}