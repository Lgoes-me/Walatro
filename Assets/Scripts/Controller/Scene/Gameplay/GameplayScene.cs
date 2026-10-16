using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using TMPro;

public class GameplayScene : BaseAwaitableScene<GameplaySceneData>
{
    [field: SerializeField] private TMP_Text LivesText { get; set; }
    [field: SerializeField] private TMP_Text ScoreToBeatText { get; set; }
    [field: SerializeField] private TMP_Text ScoreText { get; set; }

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
        
        Gameplay = GameplayService.GetNextGameplay();
        
        LivesText.text = $"Lives: {Gameplay.Vidas}";
        ScoreToBeatText.text = $"To Beat: {Gameplay.ScoreToBeat}";
        ScoreText.text = $"Score: 0";
        
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
            ScoreText.text = $"Score: {score}";

            if (!result.DidWin)
            {
                Gameplay.Vidas --;
                LivesText.text = $"Lives: {Gameplay.Vidas}";
            }
            
            if (Gameplay.Vidas == 0)
            {
                await GameOver();
                return;
            }

            if (Gameplay.ScoreToBeat <= score)
            {
                await Win();
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
        await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);
        CloseScene();
        await SceneService.WaitScene(new StoreSceneData());
    }
    
    private async UniTask GameOver()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);
        await SceneService.WaitScene(new GameOverSceneData());
        CloseScene();
        await SceneService.WaitScene(new MainMenuSceneData());
    }
}

public class GameplaySceneData : BaseSceneData
{
    public override string SceneName => "Gameplay";
}