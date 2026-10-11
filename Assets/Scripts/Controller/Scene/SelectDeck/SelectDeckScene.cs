using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SelectDeckScene : BaseAwaitableScene<SelectDeckSceneData>
{
    [field: SerializeField] private Button SelectButton { get; set; }

    protected override UniTask Init()
    {
        SelectButton.onClick.RemoveAllListeners();
        SelectButton.onClick.AddListener(() => SelectDeck().Forget());
        SelectButton.Select();

        return UniTask.CompletedTask;
    }

    private UniTask SelectDeck()
    {
        //todo Select deck
        CloseScene();

        return UniTask.CompletedTask;
    }
}

public class SelectDeckSceneData : BaseSceneData
{
    public override string SceneName => "SelectDeck";
}