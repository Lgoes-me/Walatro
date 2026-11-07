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

    [field: SerializeField] private TMP_Text PointsAddedText { get; set; }
    [field: SerializeField] private TMP_Text MultAddedText { get; set; }
    [field: SerializeField] private TMP_Text MultMultipliedText { get; set; }
    [field: SerializeField] private TMP_Text ScoreAddedText { get; set; }

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

        Gameplay.OnPointsAddedEvent += PointsAddedFeedback;
        Gameplay.OnMultAddedEvent += MultAddedFeedback;
        Gameplay.OnMultMultipliedEvent += MultMultipliedFeedback;
        Gameplay.OnScoreAddedEvent += ScoreAddedFeedback;
        
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
    
    private async void PointsAddedFeedback(int points)
    {
        PointsAddedText.gameObject.SetActive(true);
        PointsAddedText.text = $"+{points}";

        await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);

        PointsAddedText.gameObject.SetActive(false);
        PointsAddedText.text = string.Empty;
    }

    private async void MultAddedFeedback(float mult)
    {
        MultAddedText.gameObject.SetActive(true);
        MultAddedText.text = $"+{mult:00.00}";

        await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);

        MultAddedText.gameObject.SetActive(false);
        MultAddedText.text = string.Empty;
    }

    private async void MultMultipliedFeedback(float mult)
    {
        MultMultipliedText.gameObject.SetActive(true);
        MultMultipliedText.text = $"x{mult:00.00}";

        await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);

        MultMultipliedText.gameObject.SetActive(false);
        MultMultipliedText.text = string.Empty;
    }

    private async void ScoreAddedFeedback(int score)
    {
        ScoreAddedText.gameObject.SetActive(true);
        ScoreAddedText.text = $"+{score}";

        await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);
        
        ScoreAddedText.gameObject.SetActive(false);
        ScoreAddedText.text = string.Empty;
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
        await SceneService.OpenScene(new PrizeSceneData());
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
        
        Gameplay.OnPointsAddedEvent -= PointsAddedFeedback;
        Gameplay.OnMultAddedEvent -= MultAddedFeedback;
        Gameplay.OnMultMultipliedEvent -= MultMultipliedFeedback;
        Gameplay.OnScoreAddedEvent -= ScoreAddedFeedback;
    }
}

public class GameplaySceneData : BaseSceneData
{
    public override string SceneName => "Gameplay";
}