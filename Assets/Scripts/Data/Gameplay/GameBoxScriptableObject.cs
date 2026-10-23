using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameBoxScriptableObject : ScriptableObject
{
    [field: SerializeField] private List<GameDiscScriptableObject> Games { get; set; }
}