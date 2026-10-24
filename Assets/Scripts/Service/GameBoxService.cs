using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameBoxService : IService, IPreparable, IStartable
{
    public GameBox GameBox { get; set; }
    private GameSaveService GameSaveService { get; set; }

    public UniTask Prepare()
    {
        var application = Application.Instance;
        GameSaveService = application.GetService<GameSaveService>();

        return UniTask.CompletedTask;
    }

    public UniTask StartSave()
    {
        //GameBox = GameSaveService.Save.GameBox;
        return UniTask.CompletedTask;
    }

    public void SelectGameBox(GameBoxScriptableObject gameBoxScriptableObject)
    {
        GameBox = gameBoxScriptableObject.ToDomain();
    }
}