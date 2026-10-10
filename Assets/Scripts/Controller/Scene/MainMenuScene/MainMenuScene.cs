using Cysharp.Threading.Tasks;
using UnityEngine;

public class MainMenuScene : BaseAwaitableScene<MainMenuSceneData>
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
        
        //await SceneService.WaitScene(new SelectDeckSceneData());

        CloseScene();
        
        await SceneService.WaitScene(new GameplaySceneData());
    }

    public async UniTaskVoid ContinueGame()
    {
        GameSaveService.LoadLastSave();
        await Application.StartServices();
        
        CloseScene();

        await SceneService.WaitScene(new GameplaySceneData());
    }

    public void ExitGame()
    {
        UnityEngine.Application.Quit();
    }
}

public class MainMenuSceneData : BaseSceneData
{
    public override string SceneName => "MainMenu";
}