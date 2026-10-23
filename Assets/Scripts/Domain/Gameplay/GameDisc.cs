public class GameDisc
{
    public BaseMinigameController MinigameControllerPrefab { get; set; }

    public GameDisc(BaseMinigameController minigameControllerPrefab)
    {
        MinigameControllerPrefab = minigameControllerPrefab;
    }
}