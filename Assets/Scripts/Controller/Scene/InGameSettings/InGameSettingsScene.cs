using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class InGameSettingsScene : BaseAwaitableScene<InGameSettingsSceneData>
{
    protected override void Init()
    {
        
    }
}

public class InGameSettingsSceneData : BaseSceneData
{
    public override string SceneName => "InGameSettings";
}