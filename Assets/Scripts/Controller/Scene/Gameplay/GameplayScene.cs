using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using TMPro;

public class GameplayScene : BaseScene<GameplaySceneData>
{    
    [field: SerializeField] private TMP_Text LivesText { get; set; }
    [field: SerializeField] private TMP_Text ScoreToBeatText { get; set; }
    [field: SerializeField] private TMP_Text ScoreText { get; set; }

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
        
        Gameplay = GameplayService.Gameplay;

        Gameplay.OnGameplayUpdatedEvent += UpdateTexts;
        
        UpdateTexts();
        
        MinigameLoop().Forget();
    }

    private void UpdateTexts()
    {
        LivesText.text = $"Lives: {Gameplay.Vidas}";
        ScoreToBeatText.text = $"To Beat: {Gameplay.ScoreToBeat}";
        ScoreText.text = $"Score:{Gameplay.Score}";
    }

    public async UniTask MinigameLoop()
    {
        await EventsService.DoGameplayStartEvent();

        foreach(var minigame in Gameplay.Minigames)
        {
            await EventsService.DoMinigameStartEvent();

            await UniTask.Delay(TimeSpan.FromSeconds(2), ignoreTimeScale: false);

            var game = GameBoxService.GameBox.GetRandomGame();

            var result = await SceneService.WaitScene<MinigameSceneData, MinigameSceneResult>(new MinigameSceneData(game, Gameplay));

            await EventsService.DoMinigameEndEvent();

            if (!result.DidWin)
            {
                await EventsService.DoLifeLostEvent();
                Gameplay.LoseVida();
            }
            
            Gameplay.SetScore();
            
            if (Gameplay.Vidas == 0)
            {
                await GameOver();
                return;
            }

            if (Gameplay.ScoreToBeat <= Gameplay.Score)
            {
                await Win();
                return;
            }
        }

        if (Gameplay.ScoreToBeat > Gameplay.Score)
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