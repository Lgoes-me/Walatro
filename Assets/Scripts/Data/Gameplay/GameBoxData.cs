using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu]
public class GameBoxData : ScriptableObject, IIdentifiable
{
    [field: SerializeField] public string Id { get; set; }
    [field: SerializeField] private List<GameDiscData> Games { get; set; }

    public GameBox ToDomain()
    {
        var games = new List<GameDisc>();

        foreach(var gameDiscData in Games)
        {
            games.Add(gameDiscData.ToDomain());
        }

        return new GameBox(games);
    }
    
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!string.IsNullOrEmpty(Id))
            return;

        if (AssetDatabase.TryGetGUIDAndLocalFileIdentifier(this, out var guid, out _))
        {
            Id = guid;
        }
    }
#endif
}
