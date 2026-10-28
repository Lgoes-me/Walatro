using System.Collections.Generic;
using UnityEngine;

public class GameBox
{
    public List<GameDisc> Games { get; set; }

    public GameBox(List<GameDisc> games)
    {
        Games = games;
    }

    public GameDisc GetRandomGame()
    {
        return Games[Random.Range(0, Games.Count)];
    }
    
    public GameBoxModel ToModel()
    {
        var discsModels = new List<GameDiscModel>();

        foreach (var disc in Games)
        {
            discsModels.Add(disc.ToModel());
        }

        return new GameBoxModel(discsModels);
    }
}