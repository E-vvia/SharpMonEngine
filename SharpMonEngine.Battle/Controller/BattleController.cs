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
        private BattleInstance _battleInstance;

        public BattleController(BattleInstance battleInstance,
            IBattleControllerRequestComparer requestComparer,
            IDataProvider dataProvider,
            IDamageCalculationService damageCalculationService)
        {
            _battleInstance = battleInstance;
            _requestComparer = requestComparer;
            _dataProvider = dataProvider;
            _damageCalculationService = damageCalculationService;
        }

        public BattleControllerResult InitializeBattle()
        {
            BattleControllerResult result = new BattleControllerResult();
            result.PreviousState = _battleInstance.BattleState;
            _battleInstance.TurnNumber = 1;
            _battleInstance.BattleState = BattleState.WaitingInput;
            result.CurrentState = _battleInstance.BattleState;
            return result;
        }

        public BattleControllerResult DoAction(IEnumerable<BattleControllerRequest> battleControllerRequests)
        {
            return ResolveActions(battleControllerRequests);
        }

        private BattleControllerResult ResolveActions(IEnumerable<BattleControllerRequest> battleControllerRequests)
        {
            BattleControllerResult result = new BattleControllerResult();
            result.PreviousState = _battleInstance.BattleState;

            BattleControllerRequest[] arr = battleControllerRequests.ToArray();
            Array.Sort(arr, _requestComparer.GetComparer(_battleInstance));

            foreach (BattleControllerRequest r in arr)
            {
                ResolveAction(result, r);
            }

            return result;
        }

        private void ResolveAction(BattleControllerResult result, BattleControllerRequest battleControllerRequest)
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
                    DoMove(result, battleControllerRequest);
                    break;
            }
        }

        private void DoMove(BattleControllerResult result, BattleControllerRequest battleControllerRequest)
        {
            int movementId = battleControllerRequest.Args[0];
            SpeciesBattleInstance attacker =
                _battleInstance.Combatants[battleControllerRequest.Side, battleControllerRequest.Slot];

            IMoveData usedMove = _dataProvider.GetMoveData(movementId);

            foreach ((int, int) targetSideSlot in battleControllerRequest.Targets)
            {
                SpeciesBattleInstance defender =
                    _battleInstance.Combatants[targetSideSlot.Item1, targetSideSlot.Item2];

                DamageCalculationContext damageCalculationContext =
                    new DamageCalculationContext(attacker, defender, usedMove);
                int damage = _damageCalculationService.CalculateDamage(damageCalculationContext);

                result.Events.Add(new BattleControllerResultEvent()
                {
                    EventResultType = BattleControllerResultEvent.EventType.DamageDone,
                    Source = (battleControllerRequest.Side, battleControllerRequest.Slot),
                    Target = targetSideSlot,
                    Args = new int[] { damage },
                });
            }

            result.CurrentState = BattleState.WaitingInput;
        }
    }
}