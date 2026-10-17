using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScene : BaseAwaitableScene<SettingsSceneData>
{
    protected override UniTask Init()
    {
        return UniTask.CompletedTask;
    }
}

public class SettingsSceneData : BaseSceneData
{
    public override string SceneName => "Settings";
}