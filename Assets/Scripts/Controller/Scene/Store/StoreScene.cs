using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class StoreScene : BaseScene<StoreSceneData>
{
    [field: SerializeField] private Button NextButton { get; set; }
    [field: SerializeField] private Button Item1Button { get; set; }
    [field: SerializeField] private Button Item2Button { get; set; }
    [field: SerializeField] private Button Item3Button { get; set; }
    [field: SerializeField] private Button Item4Button { get; set; }
    [field: SerializeField] private Button RerollButton { get; set; }

    private SceneService SceneService { get; set; }
    private GameSaveService GameSaveService { get; set; }

    protected override void Init()
    {
        SceneService = Application.GetService<SceneService>();
        GameSaveService = Application.GetService<GameSaveService>();
        
        NextButton.onClick.RemoveAllListeners();
        NextButton.onClick.AddListener(() => Next().Forget());
        NextButton.Select();
    }

    private async UniTask Next()
    {
        await SceneService.OpenScene(new GameplaySceneData());
    }
}

public class StoreSceneData : BaseSceneData
{
    public override string SceneName => "Store";
}