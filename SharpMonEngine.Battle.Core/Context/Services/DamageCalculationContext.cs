using System.Collections.Generic;
using System.Linq;
using SharpMonEngine.Battle.Core.Model;
using SharpMonEngine.Core.Interfaces.Model;

namespace SharpMonEngine.Battle.Core.Context.Services
{
    public class DamageCalculationContext
    {
        public bool WasCriticalHit { get; set; } = false;
        public SpeciesBattleInstance Attacker { get; set; }
        public SpeciesBattleInstance Defender { get; set; }

        public IMoveData UsedMove { get; set; }

        public bool Multitarget { get; set; } = false;

        public byte Flags { get; set; } = 0;
        public byte ActiveWeather { get; set; } = 0;

        public DamageCalculationContext(SpeciesBattleInstance attacker,
            SpeciesBattleInstance defender,
            IMoveData usedMove)
        {
            Attacker = attacker;
            Defender = defender;
            UsedMove = usedMove;
        }
    }
}