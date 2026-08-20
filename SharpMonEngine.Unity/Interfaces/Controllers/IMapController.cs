using UnityEngine;

namespace SharpMonEngine.Unity.Interfaces.Controllers
{
    public interface IMapController
    {
        Vector3 GetGridPosition(Vector3 worldPosition);
    }
}