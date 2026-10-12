using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using TMPro;

public class GameplayScene : BaseAwaitableScene<GameplaySceneData>
{
    [field: SerializeField] private TMP_Text LivesText { get; set; }

    private SceneService SceneService { get; set; }
    private GameSaveService GameSaveService { get; set; }
    private GameplayService GameplayService { get; set; }

    private Gameplay Gameplay { get; set; }

    protected override async UniTask Init()
    {
        SceneService = Application.GetService<SceneService>();
        GameSaveService = Application.GetService<GameSaveService>();
        GameplayService = Application.GetService<GameplayService>();
        
        GameSaveService.SaveCurrentGameState();
        
        Gameplay = new Gameplay();
        LivesText.text = $"Lives: {Gameplay.Vidas}";
        
        await OpenMinigameLoop();
    }

    private async UniTask OpenMinigameLoop()
    {
        var score = 0;

        foreach(var minigame in Gameplay.Minigames)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(2), ignoreTimeScale: false);

            var result = await SceneService.WaitScene<MinigameSceneData, MinigameResult>(new MinigameSceneData());

            score += result.Score;

            if (!result.DidWin)
            {
                Gameplay.Vidas --;
                LivesText.text = $"Lives: {Gameplay.Vidas}";
            }
            
            if (Gameplay.Vidas == 0)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);
                await GameOver();
                return;
            }
        }

        if (Gameplay.ScoreToBeat > score)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);
            await GameOver();
            return;
        }

        await Win();
    }

    private async UniTask Win()
    {
        CloseScene();
        await SceneService.WaitScene(new StoreSceneData());
    }
    
    private async UniTask GameOver()
    {
        await SceneService.WaitScene(new GameOverSceneData());
        CloseScene();
        await SceneService.WaitScene(new MainMenuSceneData());
    }
}

public class GameplaySceneData : BaseSceneData
{
    public override string SceneName => "Gameplay";
}