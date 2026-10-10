using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class StartScene : BaseScene<StartSceneData>
{
    [field: SerializeField] private GameObject Menu { get; set; }
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

            //todo Select default onboarding deck

            Menu.SetActive(false);

            await Application.StartServices();
            await SceneService.WaitScene(new GameplaySceneData());
        }
        else
        {
            Menu.SetActive(false);
            
            await SceneService.WaitScene(new MainMenuSceneData());
        }
    }

}

public class StartSceneData : BaseSceneData
{
    public override string SceneName => "Start";
}