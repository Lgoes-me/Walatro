using System.Collections.Generic;

public class GameBox
{
    public List<GameDisc> Games { get; set; }

    public GameBox(List<GameDisc> games)
    {
        Games = games;
    }

    public GameDisc GetRandomGame()
    {
        return Games[0];
    }
}