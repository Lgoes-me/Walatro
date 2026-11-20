using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameBoxService : IService, IPreparable, IStartable
{
    public GameBox GameBox { get; set; }

    private GameSaveService GameSaveService { get; set; }
    private ContentService ContentService { get; set; }
    private InterfaceService InterfaceService { get; set; }

    public UniTask Prepare()
    {
        var application = Application.Instance;
        GameSaveService = application.GetService<GameSaveService>();
        ContentService = application.GetService<ContentService>();
        InterfaceService = application.GetService<InterfaceService>();

        return UniTask.CompletedTask;
    }

    public async UniTask StartSave()
    {
        var games = new List<GameDisc>();
        
        foreach (var gameModel in GameSaveService.Save.GameBox.Discs)
        {
            var gameDisc = await ContentService.GetGameDiscsFromModel(gameModel);
            
            InterfaceService.GameDiscManager.ShowDisc(gameDisc);
            games.Add(gameDisc);
        }
        
        GameBox = new GameBox(games);
    }

    public void SelectGameBox(GameBox gameBox)
    {
        GameBox = gameBox;

        foreach (var gameDisc in GameBox.Games)
        {
            InterfaceService.GameDiscManager.ShowDisc(gameDisc);
        }
    }
}