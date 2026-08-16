using System;
using SharpMonEngine.Core.Interfaces.Model;
using SharpMonEngine.Core.Model;
using UnityEngine;

namespace SharpMonEngine.Unity.Model
{
    [Serializable]
    public class SpeciesFormScriptableObject : ISpeciesForm
    {
        [SerializeField] private byte[] stats = new byte[(int)Stats.Count];
        [field: SerializeField] public MonType Type1 { get; set; }
        [field: SerializeField] public MonType Type2 { get; set; }
        [field: SerializeField] public int Id { get; set; }
        [field: SerializeField] public string Name { get; set; } = "SpeciesForm";

        public byte Hp
        {
            get => stats[(int)Stats.Hp];
            set => stats[(int)Stats.Hp] = value;
        }

        public byte Atk
        {
            get => stats[(int)Stats.Atk];
            set => stats[(int)Stats.Atk] = value;
        }

        public byte SpAtk
        {
            get => stats[(int)Stats.SpAtk];
            set => stats[(int)Stats.SpAtk] = value;
        }

        public byte Def
        {
            get => stats[(int)Stats.Def];
            set => stats[(int)Stats.Def] = value;
        }

        public byte SpDef
        {
            get => stats[(int)Stats.SpDef];
            set => stats[(int)Stats.SpDef] = value;
        }

        public byte Speed
        {
            get => stats[(int)Stats.Speed];
            set => stats[(int)Stats.Speed] = value;
        }

        [field: SerializeField] public float Height { get; set; }
        [field: SerializeField] public float Weight { get; set; }
    }
}