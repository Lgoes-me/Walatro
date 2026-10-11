using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GameplayScene : BaseAwaitableScene<GameplaySceneData>
{
    [field: SerializeField] private Button WinButton { get; set; }
    [field: SerializeField] private Button GameOverButton { get; set; }

    private SceneService SceneService { get; set; }
    private GameSaveService GameSaveService { get; set; }

    protected override UniTask Init()
    {
        SceneService = Application.GetService<SceneService>();
        GameSaveService = Application.GetService<GameSaveService>();
        
        GameSaveService.SaveCurrentGameState();
        
        WinButton.onClick.RemoveAllListeners();
        WinButton.onClick.AddListener(() => Win().Forget());
        WinButton.Select();

        GameOverButton.onClick.RemoveAllListeners();
        GameOverButton.onClick.AddListener(() => GameOver().Forget());

        return UniTask.CompletedTask;
    }

    private async UniTask Win()
    {
        CloseScene();
        await SceneService.WaitScene(new StoreSceneData());
    }
    
    private async UniTask GameOver()
    {
        await SceneService.WaitScene(new GameOverSceneData());

        GameSaveService.DeleteSave(GameSaveService.Save);
    
        CloseScene();
        
        await SceneService.WaitScene(new MainMenuSceneData());
    }
}

public class GameplaySceneData : BaseSceneData
{
    public override string SceneName => "Gameplay";
}