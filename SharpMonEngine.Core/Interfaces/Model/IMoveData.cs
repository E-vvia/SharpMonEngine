using SharpMonEngine.Core.Model;

namespace SharpMonEngine.Core.Interfaces.Model
{
    public interface IMoveData
    {
        public int Id { get; set; }
        public MonType Type { get; }
        public int Power { get; }
        public int Pp { get; }
    }
}