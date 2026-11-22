using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameBoxService : IService, IPreparable, IStartable
{
    public GameBox GameBox { get; set; }

    private GameSaveService GameSaveService { get; set; }
    private RandomService RandomService { get; set; }
    private ContentService ContentService { get; set; }
    private EnvironmentService EnvironmentService { get; set; }

    public GameDisc CurrentGameDisc { get; set; }

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
        
        foreach (var gameModel in GameSaveService.Save.GameBox.Discs)
        {
            var gameDisc = await ContentService.GetGameDiscsFromModel(gameModel);
            
            EnvironmentService.GameDiscManager.ShowDisc(gameDisc);
            games.Add(gameDisc);
        }
        
        GameBox = new GameBox(games);
    }

    public void SelectGameBox(GameBox gameBox)
    {
        GameBox = gameBox;

        foreach (var gameDisc in GameBox.Games)
        {
            EnvironmentService.GameDiscManager.ShowDisc(gameDisc);
        }
    }
    
    public async UniTask<GameDisc> GetRandomGame()
    {
        CurrentGameDisc = RandomService.RandomElement(GameBox.Games);
        CurrentGameDisc.TriggerGameChosenDelegate();
        
        await UniTask.Delay(TimeSpan.FromSeconds(2), ignoreTimeScale: false);

        return CurrentGameDisc;
    }
    
    public async UniTask FinishGame()
    {
        CurrentGameDisc.TriggerGameCompleteDelegate();
        await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);
        CurrentGameDisc = null;
    }
}