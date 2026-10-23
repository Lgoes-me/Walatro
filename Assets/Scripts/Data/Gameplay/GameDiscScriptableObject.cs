using UnityEngine;

[CreateAssetMenu]
public class GameDiscScriptableObject : ScriptableObject
{
    [field: SerializeField] private ResidentEvilMinigameController MinigameControllerPrefab { get; set; }

    public GameDisc ToDomain()
    {
        return new GameDisc(MinigameControllerPrefab);
    }
}