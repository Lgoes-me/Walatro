using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameplayScene : BaseAwaitableScene<GameplaySceneData>
{
    private SceneService SceneService { get; set; }
    private GameSaveService GameSaveService { get; set; }

    private List<int> Minigames { get; set; }
    private int Vidas { get; set; }
    private int ScoreToBeat { get; set; }

    protected override async UniTask Init()
    {
        SceneService = Application.GetService<SceneService>();
        GameSaveService = Application.GetService<GameSaveService>();
        
        GameSaveService.SaveCurrentGameState();
        
        Minigames = new List<int>() {1, 2, 3, 4, 5};
        Vidas = 3;
        ScoreToBeat = 500;
        
        await OpenMinigameLoop();
    }

    private async UniTask OpenMinigameLoop()
    {
        var score = 0;

        foreach(var minigame in Minigames)
        {
            var didWin = await SceneService.WaitScene<MinigameSceneData, bool>(new MinigameSceneData());

            score += 200;

            if (!didWin)
            {
                Vidas --;
            }
            
            if (Vidas == 0)
            {
                await GameOver();
                return;
            }
        }

        if (ScoreToBeat > score)
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