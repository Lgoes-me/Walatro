using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScene : BaseAwaitableScene<GameOverSceneData>
{
    [field: SerializeField] private Button ResetButton { get; set; }

    private GameSaveService GameSaveService { get; set; }

    protected override UniTask Init()
    {
        GameSaveService = Application.GetService<GameSaveService>();
        
        ResetButton.onClick.RemoveAllListeners();
        ResetButton.onClick.AddListener(() => Reset().Forget());
        ResetButton.Select();

        return UniTask.CompletedTask;
    }

    private UniTask Reset()
    {
        GameSaveService.DeleteSave(GameSaveService.Save);

        CloseScene();
        
        return UniTask.CompletedTask;
    }

}

public class GameOverSceneData : BaseSceneData
{
    public override string SceneName => "GameOver";
}
