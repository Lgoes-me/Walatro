using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using TMPro;

public class GameplayScene : BaseAwaitableScene<GameplaySceneData>
{
    [field: SerializeField] private GameBoxScriptableObject GameBox { get; set; }
    
    [field: SerializeField] private TMP_Text LivesText { get; set; }
    [field: SerializeField] private TMP_Text ScoreToBeatText { get; set; }
    [field: SerializeField] private TMP_Text ScoreText { get; set; }

    private SceneService SceneService { get; set; }
    private GameplayService GameplayService { get; set; }

    protected override  void Init()
    {
        SceneService = Application.GetService<SceneService>();
        GameplayService = Application.GetService<GameplayService>();
        
        UpdateTexts();
        
        GameplayService.MinigameLoop(GameBox.ToDomain(), UpdateTexts, Win, GameOver).Forget();
    }

    private void UpdateTexts()
    {
        var gameplay = GameplayService.Gameplay;

        LivesText.text = $"Lives: {gameplay.Vidas}";
        ScoreToBeatText.text = $"To Beat: {gameplay.ScoreToBeat}";
        ScoreText.text = $"Score:{gameplay.Score}";
    }

    private async UniTask Win()
    {
        GameplayService.NextGameplay();

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