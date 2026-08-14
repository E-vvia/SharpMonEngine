using SharpMonEngine.Core.Model;

namespace SharpMonEngine.Core.Interfaces.Model
{
    public interface ISpeciesInstance
    {
        public int SpeciesId { get; }
        public int AbilityId { get; }
        public MonType Type1 { get; }
        public MonType Type2 { get; }
        int Level { get; }
        int Atk { get; }
        int Def { get; }
        int SpAtk { get; }
        int SpDef { get; }
        int Speed { get; }

        int HpEv { get; }
        int AtkEv { get; }
        int DefEv { get; }
        int SpAtkEv { get; }
        int SpDefEv { get; }
        int SpeedEv { get; }

        int HpIv { get; }
        int AtkIv { get; }
        int DefIv { get; }
        int SpAtkIv { get; }
        int SpDefIv { get; }
        int SpeedIv { get; }
    }
}