using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameBoxService : IService, IPreparable, IStartable
{
    public GameBox GameBox { get; set; }

    private GameSaveService GameSaveService { get; set; }
    private ContentService ContentService { get; set; }

    public UniTask Prepare()
    {
        var application = Application.Instance;
        GameSaveService = application.GetService<GameSaveService>();
        ContentService = application.GetService<ContentService>();

        return UniTask.CompletedTask;
    }

    public async UniTask StartSave()
    {
        var games = new List<GameDisc>();
        
        foreach (var gameModel in GameSaveService.Save.GameBox.Discs)
        {
            games.Add(await ContentService.GetGameDiscsFromModel(gameModel));
        }
        
        GameBox = new GameBox(games);
    }

    public void SelectGameBox(GameBox gameBox)
    {
        GameBox = gameBox;
    }
}