using System;
using System.Linq;
using SharpMonEngine.Core.Interfaces.Model;
using SharpMonEngine.Core.Model;
using UnityEngine;

namespace SharpMonEngine.Unity.Model
{
    [Serializable]
    [CreateAssetMenu(fileName = "Species", menuName = "SharpMonEngine/ScriptableObjects/SpeciesScriptableObject")]
    public class SpeciesScriptableObject : ScriptableObject, ISpecies
    {
        [SerializeField] private SpeciesFormScriptableObject[] forms = Array.Empty<SpeciesFormScriptableObject>();

        [field: SerializeField] public int Id { get; set; }
        [field: SerializeField] public MonType Type1 { get; set; }
        [field: SerializeField] public MonType Type2 { get; set; }
        [field: SerializeField] public string Name { get; set; } = "Species";

        public ISpeciesForm[] Forms
        {
            get => forms.Cast<ISpeciesForm>().ToArray();
            set => forms = value
                .Cast<SpeciesFormScriptableObject>()
                .ToArray();
        }
    }
}