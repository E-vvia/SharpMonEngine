using System.Collections.Generic;
using SharpMonEngine.Battle.Core.Interfaces.Modifiers;

namespace SharpMonEngine.Model.Providers
{
    public class CalculationModifierCollection
    {
        public Dictionary<int, ICalculationModifier> MoveCalculationModifiers { get; set; }
        public Dictionary<int, ICalculationModifier> FlagCalculationModifiers { get; set; }

        public Dictionary<int, ICalculationModifier> WeatherCalculationModifiers { get; set; }
        public Dictionary<int, ICalculationModifier> MoveCriticalHitCalculationModifiers { get; set; }

        public Dictionary<int, ICalculationModifier> AbilityCriticalHitCalculationModifiers { get; set; }
        public Dictionary<int, ICalculationModifier> StatusCriticalHitCalculationModifiers { get; set; }
        public Dictionary<int, ICalculationModifier> StabCalculationModifiers { get; set; }
        public Dictionary<int, ICalculationModifier> TypeCalculationModifiers { get; set; }

        public CalculationModifierCollection(Dictionary<int, ICalculationModifier> moveCalculationModifiers,
            Dictionary<int, ICalculationModifier> flagCalculationModifiers,
            Dictionary<int, ICalculationModifier> weatherCalculationModifiers,
            Dictionary<int, ICalculationModifier> moveCriticalHitCalculationModifiers,
            Dictionary<int, ICalculationModifier> abilityCriticalHitCalculationModifiers,
            Dictionary<int, ICalculationModifier> statusCriticalHitCalculationModifiers,
            Dictionary<int, ICalculationModifier> stabCalculationModifiers,
            Dictionary<int, ICalculationModifier> typeCalculationModifiers)
        {
            MoveCalculationModifiers = moveCalculationModifiers;
            FlagCalculationModifiers = flagCalculationModifiers;
            WeatherCalculationModifiers = weatherCalculationModifiers;
            MoveCriticalHitCalculationModifiers = moveCriticalHitCalculationModifiers;
            AbilityCriticalHitCalculationModifiers = abilityCriticalHitCalculationModifiers;
            StatusCriticalHitCalculationModifiers = statusCriticalHitCalculationModifiers;
            StabCalculationModifiers = stabCalculationModifiers;
            TypeCalculationModifiers = typeCalculationModifiers;
        }
    }
}