using Moq;
using SharpMonEngine.Core.Interfaces.Model;
using SharpMonEngine.Core.Model;

namespace SharmonEngine.Battle.Test
{
    public class MockCreator
    {
        public static Mock<ISpeciesInstance> CreateLevel100BulbasaurMock()
        {
            Mock<ISpeciesInstance> species = new();

            species.Setup(s => s.Type1).Returns(MonType.Grass);
            species.Setup(s => s.Type2).Returns(MonType.Poison);
            species.Setup(s => s.Level).Returns(100);

            species.Setup(s => s.Atk).Returns(103);
            species.Setup(s => s.Def).Returns(103);
            species.Setup(s => s.SpAtk).Returns(135);
            species.Setup(s => s.SpDef).Returns(135);
            species.Setup(s => s.Speed).Returns(95);

            return species;
        }

        public static Mock<IMoveData> CreateMoveMock(int power, MonType type)
        {
            Mock<IMoveData> move = new();

            move.Setup(m => m.Power).Returns(power);
            move.Setup(m => m.Type).Returns(type);

            return move;
        }
    }
}