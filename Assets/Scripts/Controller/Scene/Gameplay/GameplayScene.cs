using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using TMPro;

public class GameplayScene : BaseScene<GameplaySceneData>
{    
    [field: SerializeField] private TMP_Text LivesText { get; set; }
    [field: SerializeField] private TMP_Text ScoreToBeatText { get; set; }
    [field: SerializeField] private TMP_Text TotalScoreText { get; set; }
    [field: SerializeField] private TMP_Text ScoreText { get; set; }
    [field: SerializeField] private TMP_Text PointsText { get; set; }
    [field: SerializeField] private TMP_Text MultText { get; set; }

    private SceneService SceneService { get; set; }
    private GameplayService GameplayService { get; set; }
    private EventsService EventsService { get; set; }
    private GameBoxService GameBoxService { get; set; }
    
    private Gameplay Gameplay { get; set; }

    protected override void Init()
    {
        SceneService = Application.GetService<SceneService>();
        GameplayService = Application.GetService<GameplayService>();
        EventsService = Application.GetService<EventsService>();
        GameBoxService = Application.GetService<GameBoxService>();
        
        Gameplay = GameplayService.CreateGameplay();

        Gameplay.SetMinigames(5);
        Gameplay.SetVidas(3);

        Gameplay.OnGameplayUpdatedEvent += UpdateTexts;
        
        UpdateTexts();
        
        MinigameLoop().Forget();
    }

    private void UpdateTexts()
    {
        LivesText.text = $"Lives: {Gameplay.Vidas}";
        ScoreToBeatText.text = $"To Beat: {Gameplay.ScoreToBeat}";
        TotalScoreText.text = $"{Gameplay.TotalScore}";
        ScoreText.text = $"{Gameplay.Score}";
        PointsText.text = $"{Gameplay.Points}";
        MultText.text = $"{Gameplay.AdditiveMult}";
    }

    public async UniTask MinigameLoop()
    {
        await EventsService.DoGameplayStartEvent();

        foreach(var minigame in Gameplay.Minigames)
        {
            await EventsService.DoMinigameStartEvent();

            var game = await GameBoxService.GameBox.GetRandomGame();

            var result = await SceneService.WaitScene<MinigameSceneData, MinigameSceneResult>(new MinigameSceneData(game));

            await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);

            await EventsService.DoMinigameEndEvent();

            if (!result.DidWin)
            {
                await EventsService.DoLifeLostEvent();

                await Gameplay.LoseVida();
            }
            
            await Gameplay.SetScore();

            await Gameplay.SetTotalScore();
            
            if (Gameplay.Vidas == 0)
            {
                await GameOver();
                return;
            }

            if (Gameplay.ScoreToBeat <= Gameplay.TotalScore)
            {
                await Win();
                return;
            }

            await Gameplay.NextMinigame();
        }

        if (Gameplay.ScoreToBeat > Gameplay.TotalScore)
        {
            await GameOver();
            return;
        }

        await Win();
    }

    private async UniTask Win()
    {
        await EventsService.DoGameplayEndEvent();

        GameplayService.NextGameplay();

        await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);
        await SceneService.OpenScene(new StoreSceneData());
    }
    
    private async UniTask GameOver()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);
        await SceneService.WaitScene(new GameOverSceneData());
        await SceneService.OpenScene(new MainMenuSceneData());
    }

    private void OnDestroy()
    {
        Gameplay.OnGameplayUpdatedEvent -= UpdateTexts;
    }
}

public class GameplaySceneData : BaseSceneData
{
    public override string SceneName => "Gameplay";
}