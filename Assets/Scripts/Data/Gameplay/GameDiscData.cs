using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu]
public class GameDiscData : ScriptableObject, IIdentifiable
{
    [field: SerializeField] public string Id { get; set; }
    [field: SerializeField] private BaseMinigameController MinigameControllerPrefab { get; set; }

    public GameDisc ToDomain()
    {
        return new GameDisc(Id, MinigameControllerPrefab);
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