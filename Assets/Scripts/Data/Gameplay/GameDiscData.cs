using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameDiscData : PrizeData<GameDisc>
{
    [field: SerializeField] private List<GameTag> Tags { get; set; }
    [field: SerializeField] private BaseMinigameController MinigameControllerPrefab { get; set; }
    [field: SerializeField] public Texture Texture { get; private set; }

    public GameDisc ToDomain(GameDiscModel gameDiscModel)
    {
        return new GameDisc(Id, Tags, gameDiscModel.Modifier, Texture, MinigameControllerPrefab);
    }

    public override GameDisc ToDomain()
    {
        return new GameDisc(Id, Tags, DiscModifier.None, Texture, MinigameControllerPrefab);
    }
}