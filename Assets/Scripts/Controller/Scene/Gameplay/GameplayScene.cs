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

            var didWin = await SceneService.WaitScene<MinigameSceneData, bool>(new MinigameSceneData());

            score += 200;

            if (!didWin)
            {
                Gameplay.Vidas --;
                LivesText.text = $"Lives: {Gameplay.Vidas}";
                await UniTask.Delay(TimeSpan.FromSeconds(2), ignoreTimeScale: false);
            }
            
            if (Gameplay.Vidas == 0)
            {
                await GameOver();
                return;
            }
        }

        if (Gameplay.ScoreToBeat > score)
        {
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