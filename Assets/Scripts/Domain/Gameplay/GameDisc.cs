using System;
using System.Collections.Generic;
using UnityEngine;

public class GameDisc: IBaseEventData, IInfoDescriptor
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public Rarity Rarity { get; private set; }
    public List<GameTag> Tags { get; private set; }
    public DiscModifier Modifier { get; private set; }
    public BaseMinigameController MinigameControllerPrefab { get; private set; }
    public Texture Texture { get; private set; }
    
    public BaseEventListener[] Events => Modifier switch
    {
        DiscModifier.ExtraChips => new BaseEventListener[]
        {
            new MinigameEndListener(null, new AddScoreEffectData(new RarityDefinedInt(new Dictionary<Rarity, int> {{Rarity.Common, 100}}))),
        },
        DiscModifier.Mult => Array.Empty<BaseEventListener>(),
        DiscModifier.Gold => Array.Empty<BaseEventListener>(),
        _ => Array.Empty<BaseEventListener>()
    };

    private string Description { get; set; }
    
    public event IBaseEventData.EventTriggered GameDiscTriggered;

    public GameDisc(
        string id,
        List<GameTag> tags,
        DiscModifier modifier,
        Texture texture,
        BaseMinigameController minigameControllerPrefab)
    {
        Id = id;
        Tags = tags;
        Modifier = modifier;
        Texture = texture;
        MinigameControllerPrefab = minigameControllerPrefab;
    }

    public void TriggerEventDelegate()
    {
        GameDiscTriggered?.Invoke();
    }
    
    public string GetDescription(Rarity rarity) => string.Format(Description ?? string.Empty, (this as IBaseEventData).GetTextParams(rarity));

    public GameDiscModel ToModel()
    {
        return new GameDiscModel(Id, Modifier);
    }
}

public enum GameTag
{
    Unknown = -1,
    Acao = 0,
    Terror = 1,
    Esporte = 2,
    Aventura = 3,
    Tiro = 4,
}

public enum DiscModifier
{
    None = -1,
    ExtraChips = 0,
    Mult = 1,
    Gold = 2,
}