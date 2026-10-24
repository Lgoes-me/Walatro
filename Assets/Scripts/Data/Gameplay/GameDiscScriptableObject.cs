using UnityEngine;

[CreateAssetMenu]
public class GameDiscScriptableObject : ScriptableObject, IIdentifiable
{
    [field: SerializeField] public string Id { get; set; }
    [field: SerializeField] private BaseMinigameController MinigameControllerPrefab { get; set; }

    public GameDisc ToDomain()
    {
        return new GameDisc(Id, MinigameControllerPrefab);
    }
}