using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GameplayScene : BaseAwaitableScene<GameplaySceneData>
{
    protected override UniTask Init()
    {
        Application.GetService<GameSaveService>().SaveCurrentGameState();
        return default;
    }
}

public class GameplaySceneData : BaseSceneData
{
    public override string SceneName => "Gameplay";
}