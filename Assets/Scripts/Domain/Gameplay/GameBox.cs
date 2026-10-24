using System;
using System.Collections.Generic;

[Serializable]
public class GameBox
{
    public string Id { get; set; }
    public List<GameDisc> Games { get; set; }

    public GameBox(
        string id,
        List<GameDisc> games)
    {
        Id = id;
        Games = games;
    }

    public GameDisc GetRandomGame()
    {
        return Games[0];
    }
}