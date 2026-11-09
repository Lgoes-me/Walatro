using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameplayService : IService, IPreparable, IStartable
{
    public List<int> Levels = new List<int>()
    {
        100,
        300, 
        800,
        2000,
        5000,
        11000,
        20000,
        35000,
        50000,
    };

    public int CurrentLevel { get; set; }
    
    public int CurrentBind { get; set; }

    public Gameplay Gameplay { get; set; }

    private GameSaveService GameSaveService { get; set; }

    public UniTask Prepare()
    {
        var application = Application.Instance;
        GameSaveService = application.GetService<GameSaveService>();

        return UniTask.CompletedTask;
    }

    [CallbackOrderAttribute(0)]
    public UniTask StartSave()
    {
        CurrentLevel = GameSaveService.Save.CurrentLevel;
        CurrentBind = GameSaveService.Save.CurrentBind;
        
        return UniTask.CompletedTask;
    }

    public Gameplay CreateGameplay()
    {
        Gameplay = new Gameplay((int) (Levels[CurrentLevel] * ModificadorDeScore(CurrentBind)));
        
        return Gameplay;
    }

    public void NextGameplay()
    {
        CurrentBind ++;

        if (CurrentBind == 3)
        {
            CurrentLevel ++;
            CurrentBind = 0;
        }
    }

    private float ModificadorDeScore(int bind)
    {
        return bind switch
        {
            0 => 1f,
            1 => 1.5f,
            2 => 2,
            _ => 0,
        };
    }
}