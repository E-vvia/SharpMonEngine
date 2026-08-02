using Moq;
using SharpMonEngine.Battle.Core.Interfaces.Services;
using SharpMonEngine.Battle.Core.Model;
using SharpMonEngine.Battle.Core.Model.Controller;
using SharpMonEngine.Battle.Core.Request.Controller;
using SharpMonEngine.Battle.Core.Result.Controller;
using SharpMonEngine.Comparers;
using SharpMonEngine.Controller;
using SharpMonEngine.Core.Interfaces.Providers;
using SharpMonEngine.Core.Model;
using SharpMonEngine.Model.Providers;
using SharpMonEngine.Providers;
using SharpMonEngine.Services;

namespace SharmonEngine.Battle.Test
{
    [TestFixture]
    public class BattleControllerTest
    {
        [SetUp]
        public void SetUp()
        {
            Mock<IDataProvider> dataProvider = new();
            dataProvider.Setup(d => d.GetMoveData(It.IsAny<int>()))
                .Returns(MockCreator.CreateMoveMock(80, MonType.Grass).Object);

            Mock<IRandomProvider> random = new();

            random.Setup(r => r.Next(2))
                .Returns(1);

            random.Setup(r => r.Next(85, 101))
                .Returns(90);

            IDamageCalculationService randomProviderServiceMock =
                new DamageCalculationService(
                    new CalculationModifierProvider(
                        new CalculationModifierCollection()),
                    random.Object);


            _controller = new BattleController(new BattleInstance()
                {
                    BattleState = BattleState.NotStarted,
                    CombatantBySide = 1,
                    Sides = 2,
                    Combatants = new SpeciesBattleInstance[,]
                    {
                        { new(MockCreator.CreateLevel100BulbasaurMock().Object) },
                        { new(MockCreator.CreateLevel100BulbasaurMock().Object) }
                    }
                }, new DefaultRequestComparer(random.Object),
                dataProvider.Object,
                randomProviderServiceMock);
        }

        private BattleController _controller;

        [Test]
        public void BattleControllerInitializes()
        {
            BattleControllerResult battleControllerResult = _controller.InitializeBattle();
            Assert.That(battleControllerResult.CurrentState, Is.EqualTo(BattleState.WaitingInput));
        }

        [Test]
        public void DoMove()
        {
            BattleControllerInitializes();

            BattleControllerResult battleControllerResult = _controller.DoAction(new[]
            {
                new BattleControllerRequest()
                {
                    InputType = BattleControllerRequestType.Move,
                    Args = [1],
                    Side = 0,
                    Slot = 0,
                    Targets = [(1, 0)]
                },
                new BattleControllerRequest()
                {
                    InputType = BattleControllerRequestType.Move,
                    Args = [1],
                    Side = 1,
                    Slot = 0,
                    Targets = [(0, 0)]
                }
            });

            Assert.That(battleControllerResult.CurrentState, Is.EqualTo(BattleState.WaitingInput));
        }
    }
}