using SharpMonEngine.Core.Model;

namespace SharpMonEngine.Core.Interfaces.Model
{
    public interface ISpecies
    {
        public int Id { get; }
        public MonType Type1 { get; }
        public MonType Type2 { get; }
        public string Name { get; }
        public ISpeciesForm[] Forms { get; }
    }
}