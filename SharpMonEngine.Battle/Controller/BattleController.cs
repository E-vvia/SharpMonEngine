using System;
using System.Collections.Generic;
using System.Linq;
using SharpMonEngine.Battle.Core.Context.Services;
using SharpMonEngine.Battle.Core.Interfaces.Comparers;
using SharpMonEngine.Battle.Core.Interfaces.Controller;
using SharpMonEngine.Battle.Core.Interfaces.Services;
using SharpMonEngine.Battle.Core.Model;
using SharpMonEngine.Battle.Core.Model.Controller;
using SharpMonEngine.Battle.Core.Request.Controller;
using SharpMonEngine.Battle.Core.Result.Controller;
using SharpMonEngine.Core.Interfaces.Model;
using SharpMonEngine.Core.Interfaces.Providers;

namespace SharpMonEngine.Controller
{
    public class BattleController : IBattleController
    {
        private readonly IDamageCalculationService _damageCalculationService;
        private readonly IDataProvider _dataProvider;
        private readonly IBattleControllerRequestComparer _requestComparer;

        public BattleController(
            IBattleControllerRequestComparer requestComparer,
            IDataProvider dataProvider,
            IDamageCalculationService damageCalculationService)
        {
            _requestComparer = requestComparer;
            _dataProvider = dataProvider;
            _damageCalculationService = damageCalculationService;
        }

        public BattleControllerResult InitializeBattle(BattleInstance battleInstance)
        {
            BattleControllerResult result = new BattleControllerResult
            {
                PreviousState = battleInstance.BattleState
            };

            battleInstance.TurnNumber = 1;
            battleInstance.BattleState = BattleState.WaitingInput;

            result.CurrentState = battleInstance.BattleState;

            return result;
        }

        public BattleControllerResult DoAction(
            BattleInstance battleInstance,
            IEnumerable<BattleControllerRequest> battleControllerRequests)
        {
            return ResolveActions(battleInstance, battleControllerRequests);
        }

        private BattleControllerResult ResolveActions(
            BattleInstance battleInstance,
            IEnumerable<BattleControllerRequest> battleControllerRequests)
        {
            BattleControllerResult result = new BattleControllerResult
            {
                PreviousState = battleInstance.BattleState
            };

            BattleControllerRequest[] requests = battleControllerRequests.ToArray();
            Array.Sort(requests, _requestComparer.GetComparer(battleInstance));

            foreach (BattleControllerRequest request in requests)
            {
                ResolveAction(battleInstance, result, request);
            }

            return result;
        }

        private void ResolveAction(
            BattleInstance battleInstance,
            BattleControllerResult result,
            BattleControllerRequest battleControllerRequest)
        {
            switch (battleControllerRequest.InputType)
            {
                case BattleControllerRequestType.Item:
                    break;

                case BattleControllerRequestType.Switch:
                    break;

                case BattleControllerRequestType.Run:
                    break;

                case BattleControllerRequestType.Move:
                    DoMove(battleInstance, result, battleControllerRequest);
                    break;
            }
        }

        private void DoMove(
            BattleInstance battleInstance,
            BattleControllerResult result,
            BattleControllerRequest battleControllerRequest)
        {
            int moveId = battleControllerRequest.Args[0];

            SpeciesBattleInstance attacker =
                battleInstance.Combatants[battleControllerRequest.Side, battleControllerRequest.Slot];

            IMoveData usedMove = _dataProvider.GetMoveData(moveId);

            foreach ((int side, int slot) in battleControllerRequest.Targets)
            {
                SpeciesBattleInstance defender =
                    battleInstance.Combatants[side, slot];

                DamageCalculationContext context =
                    new DamageCalculationContext(attacker, defender, usedMove);

                int damage = _damageCalculationService.CalculateDamage(context);

                result.Events.Add(new BattleControllerResultEvent
                {
                    EventResultType = BattleControllerResultEvent.EventType.DamageDone,
                    Source = (battleControllerRequest.Side, battleControllerRequest.Slot),
                    Target = (side, slot),
                    Args = new[] { damage }
                });
            }

            result.CurrentState = BattleState.WaitingInput;
        }
    }
}