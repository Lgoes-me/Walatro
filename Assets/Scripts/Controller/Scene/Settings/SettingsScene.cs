using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScene : BaseAwaitableScene<SettingsSceneData>
{
    protected override void Init()
    {
        
    }
}

public class SettingsSceneData : BaseSceneData
{
    public override string SceneName => "Settings";
}