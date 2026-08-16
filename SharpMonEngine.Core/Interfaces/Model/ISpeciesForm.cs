namespace SharpMonEngine.Core.Interfaces.Model
{
    public interface ISpeciesForm
    {
        public int Id { get; }
        public string Name { get; }
        public byte Hp { get; }
        public byte Atk { get; }
        public byte SpAtk { get; }
        public byte Def { get; }
        public byte SpDef { get; }
        public byte Speed { get; }
        public float Height { get; }
        public float Weight { get; }
    }
}