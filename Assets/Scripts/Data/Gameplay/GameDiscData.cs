using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu]
public class GameDiscData : ScriptableObject, IIdentifiable
{
    [field: SerializeField] public string Id { get; set; }
    [field: SerializeField] private List<GameTag> Tags { get; set; }
    [field: SerializeField] private BaseMinigameController MinigameControllerPrefab { get; set; }
    [field: SerializeField] public Texture Texture { get; private set; }

    public GameDisc ToDomain(GameDiscModel gameDiscModel)
    {
        return new GameDisc(Id, Tags, gameDiscModel.Modifier, Texture, MinigameControllerPrefab);
    }

    public GameDisc ToDomain()
    {
        return new GameDisc(Id, Tags, DiscModifier.None, Texture, MinigameControllerPrefab);
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