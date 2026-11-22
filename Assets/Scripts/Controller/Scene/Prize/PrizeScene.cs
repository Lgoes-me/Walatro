using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PrizeScene : BaseScene<PrizeSceneData>
{
    [field: SerializeField] private Button NextButton { get; set; }

    protected override void Init()
    {
        Application.GetService<EnvironmentService>().GameDiscManager.ShowAll();
        Application.GetService<EnvironmentService>().GivePrize().Forget();

        NextButton.onClick.RemoveAllListeners();
        NextButton.onClick.AddListener(() => Next().Forget());
    }

    private async UniTask Next()
    {
        await Application.GetService<SceneService>().OpenScene(new GameplaySceneData());
    }
}

public class PrizeSceneData : BaseSceneData
{
    public override string SceneName => "Prize";
}