using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameBoxScriptableObject : ScriptableObject
{
    [field: SerializeField] private List<GameDiscScriptableObject> Games { get; set; }

    public GameBox ToDomain()
    {
        var games = new List<GameDisc>();

        foreach(var gameDiscScriptableObject in Games)
        {
            games.Add(gameDiscScriptableObject.ToDomain());
        }

        return new GameBox(games);
    }
}
