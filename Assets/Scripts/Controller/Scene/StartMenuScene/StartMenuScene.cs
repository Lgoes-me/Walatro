using Cysharp.Threading.Tasks;
using UnityEngine;

public class StartMenuScene : BaseAwaitableOverlayScene<StartMenuSceneData>
{
    [field: SerializeField] private MainTabController MainTabController { get; set; }
    [field: SerializeField] private SettingsTabController SettingsTabController { get; set; }

    private GameSaveService GameSaveService { get; set; }
    private SceneService SceneService { get; set; }
    
    protected override UniTask Init()
    {
        GameSaveService = Application.GetService<GameSaveService>();
        SceneService = Application.GetService<SceneService>();
        
        OpenMainTabController();

        return default;
    }

    public void OpenMainTabController()
    {
        SettingsTabController.gameObject.SetActive(false);
        MainTabController.Init(GameSaveService, this);
    }

    public void OpenSettingsTabController()
    {
        MainTabController.gameObject.SetActive(false);
        SettingsTabController.Init(OpenMainTabController);
    }

    public async UniTaskVoid StartGame()
    {
        GameSaveService.StartNewSave();
        await Application.StartServices();
        
        CloseScene();
        
        await SceneService.StartGame();
    }

    public async UniTaskVoid ContinueGame()
    {
        GameSaveService.LoadLastSave();
        await Application.StartServices();
        
        CloseScene();

        await SceneService.StartGame();
    }

    public void ExitGame()
    {
        UnityEngine.Application.Quit();
    }
}

public class StartMenuSceneData : BaseOverlaySceneData
{
    public override string SceneName => "Start";
}