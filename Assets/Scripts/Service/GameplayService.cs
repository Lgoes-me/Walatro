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

    private GameBoxService GameBoxService { get; set; }

    private SceneService SceneService { get; set; }


    public Gameplay Gameplay { get; set; }


    public UniTask Prepare()
    {
        var application = Application.Instance;
        GameSaveService = application.GetService<GameSaveService>();
        GameBoxService = application.GetService<GameBoxService>();
        SceneService = application.GetService<SceneService>();

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

    public async UniTask MinigameLoop(Action updateTexts, Func<UniTask> win, Func<UniTask> gameOver)
    {
        foreach(var minigame in Gameplay.Minigames)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(2), ignoreTimeScale: false);

            var game = GameBoxService.GameBox.GetRandomGame();

            var result = await SceneService.WaitScene<MinigameSceneData, MinigameSceneResult>(new MinigameSceneData(game));

            if (!result.DidWin)
            {
                Gameplay.Vidas --;
            }
            
            Gameplay.Score += result.Score;

            updateTexts();

            if (Gameplay.Vidas == 0)
            {
                await gameOver();
                return;
            }

            if (Gameplay.ScoreToBeat <= Gameplay.Score)
            {
                await win();
                return;
            }
        }

        if (Gameplay.ScoreToBeat > Gameplay.Score)
        {
            await gameOver();
            return;
        }

        await win();
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