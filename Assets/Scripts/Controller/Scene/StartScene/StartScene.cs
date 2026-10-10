using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class StartScene : BaseAwaitableScene<StartSceneData>
{
    [field: SerializeField] private Button StartButton { get; set; }

    private GameSaveService GameSaveService { get; set; }
    private SceneService SceneService { get; set; }
    
    protected override UniTask Init()
    {
        GameSaveService = Application.GetService<GameSaveService>();
        SceneService = Application.GetService<SceneService>();
        
        StartButton.onClick.RemoveAllListeners();
        StartButton.onClick.AddListener(() => StartGame().Forget());
        StartButton.Select();
        return default;
    }

    private async UniTask StartGame()
    {
        if (GameSaveService.IsFirstTimePlaying())
        {
            GameSaveService.StartNewSave();

            await Application.StartServices();
        
            CloseScene();
            await SceneService.WaitScene(new GameplaySceneData());
        }
        else
        {
            GameSaveService.LoadLastSave();

            await Application.StartServices();
        
            CloseScene();
            await SceneService.WaitScene(new MainMenuSceneData());
        }
    }

}

public class StartSceneData : BaseSceneData
{
    public override string SceneName => "Start";
}