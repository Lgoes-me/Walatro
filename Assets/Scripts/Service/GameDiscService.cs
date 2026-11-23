using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameDiscService : IService, IPreparable, IStartable
{
    public List<GameDisc> Games { get; set; }
    public GameDisc CurrentGameDisc { get; set; }

    private GameSaveService GameSaveService { get; set; }
    private RandomService RandomService { get; set; }
    private ContentService ContentService { get; set; }
    private EnvironmentService EnvironmentService { get; set; }

    public List<GameDisc> AvailableGames { get; set; }
    public List<GameDisc> DiscardedGames { get; set; }
    
    public UniTask Prepare()
    {
        var application = Application.Instance;
        GameSaveService = application.GetService<GameSaveService>();
        RandomService = application.GetService<RandomService>();
        ContentService = application.GetService<ContentService>();
        EnvironmentService = application.GetService<EnvironmentService>();

        return UniTask.CompletedTask;
    }

    public async UniTask StartSave()
    {
        var games = new List<GameDisc>();
        
        foreach (var gameModel in GameSaveService.Save.Games)
        {
            var gameDisc = await ContentService.GetGameDiscsFromModel(gameModel);
            
            EnvironmentService.GameDiscManager.ShowDisc(gameDisc);
            games.Add(gameDisc);
        }
        
        Games = games;
    }

    public void SelectGameBox(List<GameDisc> games)
    {
        Games = games;

        foreach (var gameDisc in Games)
        {
            EnvironmentService.GameDiscManager.ShowDisc(gameDisc);
        }
    }
    
    public async UniTask<GameDisc> GetRandomGame()
    {
        CurrentGameDisc = null;
        AvailableGames ??= new List<GameDisc>(Games);
        DiscardedGames ??= new List<GameDisc>();

        CurrentGameDisc = RandomService.RandomElement(AvailableGames);
        CurrentGameDisc.TriggerGameChosenDelegate();
        
        AvailableGames.Remove(CurrentGameDisc);
        DiscardedGames.Add(CurrentGameDisc);

        await UniTask.Delay(TimeSpan.FromSeconds(2), ignoreTimeScale: false);

        return CurrentGameDisc;
    }
    
    public async UniTask FinishGame()
    {
        CurrentGameDisc.TriggerGameCompleteDelegate();
        await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);
        CurrentGameDisc = null;
    }
    
    public void FinishGamemeplay()
    {
        AvailableGames = null;
        DiscardedGames= null;
    }
}