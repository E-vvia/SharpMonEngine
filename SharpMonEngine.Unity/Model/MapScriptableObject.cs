using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace SharpMonEngine.Unity.Model
{
    public class MapScriptableObject : ScriptableObject
    {
        public int Id { get; private set; }
        public string Name { get; set; } = string.Empty;
        public List<Tilemap> Tilemaps { get; private set; } = new List<Tilemap>();
    }
}