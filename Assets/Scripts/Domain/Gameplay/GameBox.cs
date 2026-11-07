using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameBox
{
    public List<GameDisc> Games { get; set; }

    public GameBox(List<GameDisc> games)
    {
        Games = games;
    }

    public async UniTask<GameDisc> GetRandomGame()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(2), ignoreTimeScale: false);
        return Games[UnityEngine.Random.Range(0, Games.Count)];
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