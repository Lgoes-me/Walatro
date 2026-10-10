using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScene : BaseAwaitableScene<GameOverSceneData>
{
    [field: SerializeField] private Button ResetButton { get; set; }


    protected override UniTask Init()
    {
        ResetButton.onClick.RemoveAllListeners();
        ResetButton.onClick.AddListener(() => Reset().Forget());
        ResetButton.Select();

        return default;
    }

    private async UniTask Reset()
    {
        //todo Select deck
        CloseScene();
    }

}

public class GameOverSceneData : BaseSceneData
{
    public override string SceneName => "GameOver";
}
