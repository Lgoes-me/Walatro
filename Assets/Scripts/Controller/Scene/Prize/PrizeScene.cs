using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PrizeScene : BaseScene<PrizeSceneData>
{
    [field: SerializeField] private Button NextButton { get; set; }

    private SceneService SceneService { get; set; }

    protected override void Init()
    {
        SceneService = Application.GetService<SceneService>();
        
        Application.GetService<InterfaceService>().PrizeCanvas.Show();

        NextButton.onClick.RemoveAllListeners();
        NextButton.onClick.AddListener(() => Next().Forget());
    }

    private async UniTask Next()
    {
        await SceneService.OpenScene(new GameplaySceneData());
    }
}

public class PrizeSceneData : BaseSceneData
{
    public override string SceneName => "Prize";
}