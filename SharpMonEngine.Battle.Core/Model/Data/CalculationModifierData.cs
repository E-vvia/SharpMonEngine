using System.Collections.Generic;
using SharpMonEngine.Battle.Core.Interfaces.Modifiers;

namespace SharpMonEngine.Battle.Core.Model.Data
{
    public class CalculationModifierData
    {
        public Dictionary<int, ICalculationModifier> MoveCalculationModifiers { get; internal set; } =
            new Dictionary<int, ICalculationModifier>();

        public Dictionary<int, ICalculationModifier> FlagCalculationModifiers { get; internal set; } =
            new Dictionary<int, ICalculationModifier>();

        public Dictionary<int, ICalculationModifier> WeatherCalculationModifiers { get; internal set; } =
            new Dictionary<int, ICalculationModifier>();

        public Dictionary<int, ICalculationModifier> MoveCriticalHitCalculationModifiers { get; internal set; } =
            new Dictionary<int, ICalculationModifier>();

        public Dictionary<int, ICalculationModifier> AbilityCriticalHitCalculationModifiers { get; internal set; } =
            new Dictionary<int, ICalculationModifier>();

        public Dictionary<int, ICalculationModifier> StatusCriticalHitCalculationModifiers { get; internal set; } =
            new Dictionary<int, ICalculationModifier>();

        public Dictionary<int, ICalculationModifier> StabCalculationModifiers { get; internal set; } =
            new Dictionary<int, ICalculationModifier>();

        public Dictionary<int, ICalculationModifier> TypeCalculationModifiers { get; internal set; } =
            new Dictionary<int, ICalculationModifier>();
    }
}