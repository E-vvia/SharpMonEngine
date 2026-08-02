using Moq;
using SharpMonEngine.Battle.Core.Context.Services;
using SharpMonEngine.Battle.Core.Interfaces.Services;
using SharpMonEngine.Battle.Core.Model;
using SharpMonEngine.Core.Interfaces.Model;
using SharpMonEngine.Core.Interfaces.Providers;
using SharpMonEngine.Core.Model;
using SharpMonEngine.Model.Providers;
using SharpMonEngine.Providers;
using SharpMonEngine.Services;

namespace SharmonEngine.Battle.Test
{
    [TestFixture]
    public class DamageCalculationServiceTest
    {
        public class DamageTestCase
        {
            public int Power { get; init; }
            public MonType Type { get; init; }
            public int RandomRoll { get; init; }
            public int ExpectedDamage { get; init; }

            public override string ToString()
            {
                return $"{Power} power {Type} roll {RandomRoll} => {ExpectedDamage}";
            }
        }

        private static IEnumerable<DamageTestCase> CreateDamageCases(
            int power,
            MonType type,
            params (int Roll, int Damage)[] rolls)
        {
            foreach (var (roll, damage) in rolls)
            {
                yield return new DamageTestCase
                {
                    Power = power,
                    Type = type,
                    RandomRoll = roll,
                    ExpectedDamage = damage
                };
            }
        }

        public static IEnumerable<DamageTestCase> DamageCases()
        {
            foreach (var testCase in CreateDamageCases(
                         80,
                         MonType.Normal,
                         (85, 58),
                         (86, 59),
                         (87, 60),
                         (88, 60),
                         (89, 61),
                         (90, 62),
                         (91, 62),
                         (92, 63),
                         (93, 64),
                         (94, 64),
                         (95, 65),
                         (96, 66),
                         (97, 66),
                         (98, 67),
                         (99, 68),
                         (100, 69)))
            {
                yield return testCase;
            }

            foreach (var testCase in CreateDamageCases(
                         80,
                         MonType.Grass,
                         (85, 21),
                         (86, 22),
                         (87, 22),
                         (88, 22),
                         (89, 23),
                         (90, 23),
                         (91, 23),
                         (92, 23),
                         (93, 24),
                         (94, 24),
                         (95, 24),
                         (96, 24),
                         (97, 25),
                         (98, 25),
                         (99, 25),
                         (100, 25)))
            {
                yield return testCase;
            }
        }


        [Test, TestCaseSource(nameof(DamageCases))]
        public void CalculateDamageReturnsExpectedDamage(DamageTestCase testCase)
        {
            Mock<IRandomProvider> random = new();

            random.Setup(r => r.Next(2))
                .Returns(1);

            random.Setup(r => r.Next(85, 101))
                .Returns(testCase.RandomRoll);

            IDamageCalculationService service =
                new DamageCalculationService(
                    new CalculationModifierProvider(
                        new CalculationModifierCollection()),
                    random.Object);

            SpeciesBattleInstance attacker =
                new(MockCreator.CreateLevel100BulbasaurMock().Object);

            SpeciesBattleInstance defender =
                new(MockCreator.CreateLevel100BulbasaurMock().Object);

            IMoveData move =
                MockCreator.CreateMoveMock(testCase.Power, testCase.Type).Object;

            DamageCalculationContext context =
                new(attacker, defender, move);

            int? damage = service.CalculateDamage(context);

            Assert.That(damage, Is.EqualTo(testCase.ExpectedDamage));
        }
    }
}