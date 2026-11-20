using System.Collections.Generic;
using UnityEngine;

public class GameDisc
{
    public string Id { get; set; }
    public List<GameTag> Tags { get; set; }
    public DiscModifier Modifier { get; set; }
    public BaseMinigameController MinigameControllerPrefab { get; set; }
    public Texture Texture { get; private set; }
    
    public delegate void EventTriggered();
    public event EventTriggered GameDiscTriggered;

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