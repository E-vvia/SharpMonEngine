using SharpMonEngine.Core.Interfaces.Model;
using SharpMonEngine.Core.Model;

namespace SharpMonEngine.Battle.Core.Model
{
    public class SpeciesBattleInstance
    {
        private readonly ISpeciesInstance _speciesInstance;
        public int AbilityId => _speciesInstance.AbilityId;
        public MonType Type1 => _speciesInstance.Type1;
        public MonType Type2 => _speciesInstance.Type1;
        public int Level => _speciesInstance.Level;
        public int CurrentHp { get; set; }
        public double CurrentAtk => GetCurrentStatValue(AtkLevel, _speciesInstance.Atk);
        public int AtkLevel { get; set; }
        public double CurrentDef => GetCurrentStatValue(DefLevel, _speciesInstance.Def);
        public int DefLevel { get; set; }
        public double CurrentSpAtk => GetCurrentStatValue(SpAtkLevel, _speciesInstance.SpAtk);
        public int SpAtkLevel { get; set; }
        public double CurrentSpDef => GetCurrentStatValue(SpDefLevel, _speciesInstance.SpDef);
        public int SpDefLevel { get; set; }
        public double CurrentSpeed => GetCurrentStatValue(SpeedLevel, _speciesInstance.Speed);
        public int SpeedLevel { get; set; }
        public int EvasionLevel { get; set; }
        public byte Status { get; set; }

        public SpeciesBattleInstance(ISpeciesInstance speciesInstance)
        {
            _speciesInstance = speciesInstance;
        }

        private double GetCurrentStatValue(int level, int baseValue)
        {
            switch (level)
            {
                case -6:
                    return baseValue * 2.0 / 8;
                case -5:
                    return baseValue * 2.0 / 7;
                case -4:
                    return baseValue * 2.0 / 6;
                case -3:
                    return baseValue * 2.0 / 5;
                case -2:
                    return baseValue * 2.0 / 4;
                case -1:
                    return baseValue * 2.0 / 3;
                case 0:
                    return baseValue * 2.0 / 2;
                case 1:
                    return baseValue * 3.0 / 2;
                case 2:
                    return baseValue * 4.0 / 2;
                case 3:
                    return baseValue * 5.0 / 2;
                case 4:
                    return baseValue * 6.0 / 2;
                case 5:
                    return baseValue * 7.0 / 2;
                case 6:
                    return baseValue * 8.0 / 2;
                default:
                    return baseValue;
            }
        }
    }
}