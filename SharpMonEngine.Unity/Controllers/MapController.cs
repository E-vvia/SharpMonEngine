using SharpMonEngine.Unity.Attributes;
using SharpMonEngine.Unity.Interfaces.Controllers;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace SharpMonEngine.Unity.Controllers
{
    [RequireComponent(typeof(Grid))]
    [RequireComponent(typeof(Tilemap))]
    [RequireComponent(typeof(TilemapRenderer))]
    [DefaultExecutionOrder(-99)]
    public class MapController : MonoBehaviour, IMapController
    {
        [SerializeField] [ReadOnly] private Grid _grid = null!;

        public void Awake()
        {
            ControllerContainer.Register<IMapController, MapController>(this);
        }

        public void Start()
        {
            _grid = GetComponent<Grid>();
        }

        public Vector3 GetGridPosition(Vector3 worldPosition)
        {
            Vector3Int cellPosition = _grid.WorldToCell(worldPosition);
            return _grid.GetCellCenterWorld(cellPosition);
        }
    }
}