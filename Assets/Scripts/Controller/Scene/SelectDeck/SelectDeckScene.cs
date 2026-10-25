using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SelectDeckScene : BaseAwaitableScene<SelectDeckSceneData>
{
    [field: SerializeField] private Button SelectButton { get; set; }

    private ContentService ContentService { get; set; }
    private GameBoxService GameBoxService { get; set; }

    protected override void Init()
    {
        ContentService = Application.GetService<ContentService>();
        GameBoxService = Application.GetService<GameBoxService>();
        
        SelectButton.onClick.RemoveAllListeners();
        SelectButton.onClick.AddListener(() => SelectDeck().Forget());
        SelectButton.Select();
    }

    private async UniTask SelectDeck()
    {
        var defaultGameBox = await ContentService.GetGameBoxFromId("5ee07483df0c80c4898bb4e5ee96d77f");
        GameBoxService.SelectGameBox(defaultGameBox);

        CloseScene();
    }
}

public class SelectDeckSceneData : BaseSceneData
{
    public override string SceneName => "SelectDeck";
}