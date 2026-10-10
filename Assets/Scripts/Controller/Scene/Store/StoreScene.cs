using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class StoreScene : BaseAwaitableScene<StoreSceneData>
{
    [field: SerializeField] private Button NextButton { get; set; }

    private SceneService SceneService { get; set; }
    private GameSaveService GameSaveService { get; set; }

    protected override UniTask Init()
    {
        SceneService = Application.GetService<SceneService>();
        GameSaveService = Application.GetService<GameSaveService>();
        
        GameSaveService.SaveCurrentGameState();
        
        NextButton.onClick.RemoveAllListeners();
        NextButton.onClick.AddListener(() => Next().Forget());
        NextButton.Select();

        return default;
    }

    private async UniTask Next()
    {
        CloseScene();
        
        await SceneService.WaitScene(new GameplaySceneData());
    }
}

public class StoreSceneData : BaseSceneData
{
    public override string SceneName => "Store";
}