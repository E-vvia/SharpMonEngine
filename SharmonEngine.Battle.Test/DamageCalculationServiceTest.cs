using Moq;
using SharmonEngine.Battle.Test.Services;
using SharpMonEngine.Battle.Core.Context.Services;
using SharpMonEngine.Battle.Core.Interfaces.Providers;
using SharpMonEngine.Battle.Core.Interfaces.Services;
using SharpMonEngine.Battle.Core.Model;
using SharpMonEngine.Core.Interfaces.Model;
using SharpMonEngine.Core.Interfaces.Services;
using SharpMonEngine.Core.Model;
using SharpMonEngine.Model.Providers;
using SharpMonEngine.Providers;
using SharpMonEngine.Services;

namespace SharmonEngine.Battle.Test
{
    [TestFixture]
    public class DamageCalculationServiceTest
    {
        private const int MovePower = 80;

        private IDamageCalculationService? _damageCalculationService;

        [SetUp]
        public void SetUp()
        {
            CalculationModifierCollection calculationModifierCollection = new CalculationModifierCollection();
            ICalculationModifierProvider calculationModifierProvider =
                new CalculationModifierProvider(calculationModifierCollection);

            _damageCalculationService = new DamageCalculationService(
                calculationModifierProvider,
                new CalculationServiceRandomProviderService());
        }

        private static Mock<ISpeciesInstance> CreateLevel100BulbasaurMock()
        {
            Mock<ISpeciesInstance> speciesInstanceMoq = new Mock<ISpeciesInstance>();

            speciesInstanceMoq.Setup(s => s.Type1).Returns(MonType.Grass);
            speciesInstanceMoq.Setup(s => s.Type2).Returns(MonType.Poison);
            speciesInstanceMoq.Setup(s => s.Level).Returns(100);

            speciesInstanceMoq.Setup(s => s.Atk).Returns(103);
            speciesInstanceMoq.Setup(s => s.Def).Returns(103);
            speciesInstanceMoq.Setup(s => s.SpAtk).Returns(135);
            speciesInstanceMoq.Setup(s => s.SpDef).Returns(135);
            speciesInstanceMoq.Setup(s => s.Speed).Returns(95);

            return speciesInstanceMoq;
        }

        private static Mock<IMoveData> CreateTackleMock()
        {
            Mock<IMoveData> moveMoq = new Mock<IMoveData>();
            moveMoq.SetupAllProperties();

            moveMoq.Setup(m => m.Power).Returns(MovePower);
            moveMoq.Setup(m => m.Type).Returns(MonType.Normal);

            return moveMoq;
        }

        [TestCase(85, 58)]
        [TestCase(86, 59)]
        [TestCase(87, 60)]
        [TestCase(88, 60)]
        [TestCase(89, 61)]
        [TestCase(90, 62)]
        [TestCase(91, 62)]
        [TestCase(92, 63)]
        [TestCase(93, 64)]
        [TestCase(94, 64)]
        [TestCase(95, 65)]
        [TestCase(96, 66)]
        [TestCase(97, 66)]
        [TestCase(98, 67)]
        [TestCase(99, 68)]
        [TestCase(100, 69)]
        public void CalculateDamageReturnsExpectedDamage( int randomRoll, int expectedResult)
        {
            Mock<IRandomProviderService> randomMoq = new Mock<IRandomProviderService>();
            randomMoq.Setup(r => r.Next(0, 100)).Returns(0);
            randomMoq.Setup(r => r.Next(85, 101)).Returns(randomRoll);

            IDamageCalculationService damageCalculationService = new DamageCalculationService(
                new CalculationModifierProvider(new CalculationModifierCollection()),
                randomMoq.Object);

            Mock<ISpeciesInstance> attackerSpecies = CreateLevel100BulbasaurMock();
            Mock<ISpeciesInstance> defenderSpecies = CreateLevel100BulbasaurMock();
            SpeciesBattleInstance attacker = new SpeciesBattleInstance(attackerSpecies.Object);
            SpeciesBattleInstance defender = new SpeciesBattleInstance(defenderSpecies.Object);

            Mock<IMoveData> tackle = CreateTackleMock();
            DamageCalculationContext context = new DamageCalculationContext(attacker, defender, tackle.Object);

            double? damage = damageCalculationService.CalculateDamage(context);

            Assert.That(damage, Is.Not.Null);
            Assert.That(damage, Is.Not.NaN);
            Assert.That(damage, Is.EqualTo(expectedResult));
        }
    }
}