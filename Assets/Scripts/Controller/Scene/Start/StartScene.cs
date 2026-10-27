using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class StartScene : BaseScene<StartSceneData>
{
    [field: SerializeField] private Button StartButton { get; set; }

    private GameSaveService GameSaveService { get; set; }
    private ContentService ContentService { get; set; }
    private GameBoxService GameBoxService { get; set; }
    private TutorialService TutorialService { get; set; }
    private SceneService SceneService { get; set; }
    
    protected override void Init()
    {
        GameSaveService = Application.GetService<GameSaveService>();
        ContentService = Application.GetService<ContentService>();
        GameBoxService = Application.GetService<GameBoxService>();
        TutorialService = Application.GetService<TutorialService>();
        SceneService = Application.GetService<SceneService>();
        
        StartButton.onClick.RemoveAllListeners();
        StartButton.onClick.AddListener(() => StartGame().Forget());
        StartButton.Select();
    }

    private async UniTask StartGame()
    {
        Application.GetService<InterfaceService>().StartGame();

        if (!TutorialService.HasFinishedTutorial())
        {
            if (!GameSaveService.PossuiContinue())
            {
                GameSaveService.StartNewSave();
                await Application.StartServices();

                var defaultGameBox = await ContentService.GetGameBoxFromId("5ee07483df0c80c4898bb4e5ee96d77f");
                GameBoxService.SelectGameBox(defaultGameBox);
            }
            else
            {
                GameSaveService.LoadLastSave();
                await Application.StartServices();
            }

            await SceneService.OpenScene(new StoreSceneData());
        }
        else
        {
            await SceneService.OpenScene(new MainMenuSceneData());
        }
    }

}

public class StartSceneData : BaseSceneData
{
    public override string SceneName => "Start";
}