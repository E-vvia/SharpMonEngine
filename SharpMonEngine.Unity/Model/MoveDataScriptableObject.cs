using System;
using SharpMonEngine.Core.Interfaces.Model;
using SharpMonEngine.Core.Model;
using UnityEngine;

namespace SharpMonEngine.Unity.Model
{
    [Serializable]
    [CreateAssetMenu(fileName = "Move", menuName = "SharpMonEngine/ScriptableObjects/MoveDataScriptableObject")]
    public class MoveDataScriptableObject : ScriptableObject, IMoveData
    {
        [field: SerializeField] public int Id { get; set; }
        [field: SerializeField] public MonType Type { get; set; }
        [field: SerializeField] public int Power { get; set; }
        [field: SerializeField] public int Pp { get; set; }
    }
}