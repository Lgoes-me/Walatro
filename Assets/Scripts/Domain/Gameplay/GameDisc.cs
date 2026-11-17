using System.Collections.Generic;

public class GameDisc
{
    public string Id { get; set; }
    public List<GameTag> Tags { get; set; }
    public BaseMinigameController MinigameControllerPrefab { get; set; }

    public GameDisc(
        string id,
        List<GameTag> tags,
        BaseMinigameController minigameControllerPrefab)
    {
        Id = id;
        Tags = tags;
        MinigameControllerPrefab = minigameControllerPrefab;
    }

    public GameDiscModel ToModel()
    {
        return new GameDiscModel(Id);
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