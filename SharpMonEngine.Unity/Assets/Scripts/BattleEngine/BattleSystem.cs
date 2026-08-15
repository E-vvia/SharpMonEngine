using SharpMonEngine.Battle.Impl;
using SharpMonEngine.Battle.Impl.Configuration;
using UnityEngine;

namespace BattleEngine
{
    public class BattleSystem : MonoBehaviour
    {
        private SharpMonBattleEngine _sharpMonBattleEngine;

        void Start()
        {
            _sharpMonBattleEngine = new SharpMonBattleEngine();
            var test = _sharpMonBattleEngine.CreateWildBattle(new BattleConfiguration()
            {
                Sides = 2,
                SlotsPerSide = 1
            });
        }

        void Update()
        {
        }

        private void OnDestroy()
        {
            _sharpMonBattleEngine?.Dispose();
        }
    }
}