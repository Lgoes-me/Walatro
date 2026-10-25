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

    private GameSaveService GameSaveService { get; set; }

    public Gameplay Gameplay { get; set; }


    public UniTask Prepare()
    {
        var application = Application.Instance;
        GameSaveService = application.GetService<GameSaveService>();

        return UniTask.CompletedTask;
    }

    public UniTask StartSave()
    {
        CurrentLevel = GameSaveService.Save.CurrentLevel;
        CurrentBind = GameSaveService.Save.CurrentBind;

        Gameplay = new Gameplay((int) (Levels[CurrentLevel] * ModificadorDeScore(CurrentBind)));

        return UniTask.CompletedTask;
    }

    public void NextGameplay()
    {
        CurrentBind ++;

        if (CurrentBind == 3)
        {
            CurrentLevel ++;
            CurrentBind = 0;
        }
        
        Gameplay = new Gameplay((int) (Levels[CurrentLevel] * ModificadorDeScore(CurrentBind)));
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