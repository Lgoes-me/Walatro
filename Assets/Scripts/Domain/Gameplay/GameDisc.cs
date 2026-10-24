public class GameDisc
{
    public string Id { get; set; }
    public BaseMinigameController MinigameControllerPrefab { get; set; }

    public GameDisc(
        string id,
        BaseMinigameController minigameControllerPrefab)
    {
        Id = id;
        MinigameControllerPrefab = minigameControllerPrefab;
    }

    public GameDiscModel ToModel()
    {
        return new GameDiscModel(Id);
    }
}