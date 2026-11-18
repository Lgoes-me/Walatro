using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScene : BaseAwaitableScene<GameOverSceneData>
{
    [field: SerializeField] private Button ResetButton { get; set; }

    private GameSaveService GameSaveService { get; set; }

    protected override void Init()
    {
        GameSaveService = Application.GetService<GameSaveService>();
        
        ResetButton.onClick.RemoveAllListeners();
        ResetButton.onClick.AddListener(() => Reset().Forget());
        ResetButton.Select();
    }

    private async UniTask Reset()
    {
        await Application.OnGameOver();   
        CloseScene();
    }
}

public class GameOverSceneData : BaseSceneData
{
    public override string SceneName => "GameOver";
}
