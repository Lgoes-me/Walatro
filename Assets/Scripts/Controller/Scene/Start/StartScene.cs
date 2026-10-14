using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class StartScene : BaseScene<StartSceneData>
{
    [field: SerializeField] private Canvas Menu { get; set; }
    [field: SerializeField] private Button StartButton { get; set; }

    private GameSaveService GameSaveService { get; set; }
    private TutorialService TutorialService { get; set; }
    private SceneService SceneService { get; set; }
    
    protected override UniTask Init()
    {
        Menu.worldCamera = Application.MainCamera;

        GameSaveService = Application.GetService<GameSaveService>();
        TutorialService = Application.GetService<TutorialService>();
        SceneService = Application.GetService<SceneService>();
        
        StartButton.onClick.RemoveAllListeners();
        StartButton.onClick.AddListener(() => StartGame().Forget());
        StartButton.Select();

        return UniTask.CompletedTask;
    }

    private async UniTask StartGame()
    {
        if (!TutorialService.HasFinishedTutorial())
        {
            if (!GameSaveService.PossuiContinue())
            {
                GameSaveService.StartNewSave();
                //todo Select default onboarding deck
            }
            else
            {
                GameSaveService.LoadLastSave();
            }

            Menu.gameObject.SetActive(false);

            await Application.StartServices();
            await SceneService.WaitScene(new StoreSceneData());
        }
        else
        {
            Menu.gameObject.SetActive(false);
            
            await SceneService.WaitScene(new MainMenuSceneData());
        }
    }

}

public class StartSceneData : BaseSceneData
{
    public override string SceneName => "Start";
}