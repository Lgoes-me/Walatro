using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScene : BaseAwaitableScene<MainMenuSceneData>
{
    [field: SerializeField] private Button ContinueButton { get; set; }
    [field: SerializeField] private Button StartButton { get; set; }
    [field: SerializeField] private Button SettingsButton { get; set; }
    [field: SerializeField] private Button ExitButton { get; set; }

    private GameSaveService GameSaveService { get; set; }
    private SceneService SceneService { get; set; }
    
    protected override UniTask Init()
    {
        GameSaveService = Application.GetService<GameSaveService>();
        SceneService = Application.GetService<SceneService>();
        
        if (GameSaveService.PossuiContinue())
        {
            ContinueButton.gameObject.SetActive(true);
            ContinueButton.Select();
        }
        else
        {
            ContinueButton.gameObject.SetActive(false);
            StartButton.Select();
        }
    
        ContinueButton.onClick.RemoveAllListeners();
        ContinueButton.onClick.AddListener(() => ContinueGame().Forget());

        StartButton.onClick.RemoveAllListeners();
        StartButton.onClick.AddListener(() => StartGame().Forget());

        //SettingsButton.onClick.RemoveAllListeners();
        //SettingsButton.onClick.AddListener(scene.OpenSettingsTabController);

        ExitButton.onClick.RemoveAllListeners();
        ExitButton.onClick.AddListener(ExitGame);

        return UniTask.CompletedTask;
    }

    private async UniTaskVoid StartGame()
    {
        if (GameSaveService.PossuiContinue())
        {
            GameSaveService.LoadLastSave();
            GameSaveService.DeleteSave(GameSaveService.Save);
        }

        GameSaveService.StartNewSave();
        await Application.StartServices();
        
        await SceneService.WaitScene(new SelectDeckSceneData());

        CloseScene();
        
        await SceneService.WaitScene(new GameplaySceneData());
    }

    private async UniTaskVoid ContinueGame()
    {
        GameSaveService.LoadLastSave();
        await Application.StartServices();
        
        CloseScene();

        await SceneService.WaitScene(new GameplaySceneData());
    }

    private void ExitGame()
    {
        UnityEngine.Application.Quit();
    }
}

public class MainMenuSceneData : BaseSceneData
{
    public override string SceneName => "MainMenu";
}