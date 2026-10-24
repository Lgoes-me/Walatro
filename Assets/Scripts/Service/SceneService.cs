using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneService : IService, IPreparable
{
    private Application Application { get; set; }

    public UniTask Prepare()
    {
        Application = Application.Instance;
        return UniTask.CompletedTask;
    }

    public async UniTask OpenScene<T>(T sceneData) where T : BaseSceneData
    {
        await SceneManager.LoadSceneAsync(sceneData.SceneName, LoadSceneMode.Single);
        UnityEngine.Object.FindAnyObjectByType<BaseScene<T>>().OpenScene(Application, sceneData);
    }

    public async UniTask WaitScene<T>(T sceneData) where T : BaseSceneData
    {
        await SceneManager.LoadSceneAsync(sceneData.SceneName, LoadSceneMode.Additive);
        await UnityEngine.Object.FindAnyObjectByType<BaseAwaitableScene<T>>().WaitOpenScene(Application, sceneData);
        await SceneManager.UnloadSceneAsync(sceneData.SceneName);
    }

    public async UniTask<T2> WaitScene<T, T2>(T sceneData) where T : BaseSceneData
    {
        await SceneManager.LoadSceneAsync(sceneData.SceneName, LoadSceneMode.Additive);
        var result = await UnityEngine.Object.FindAnyObjectByType<BaseAwaitableSceneWithResponse<T, T2>>().WaitOpenScene(Application, sceneData);
        await SceneManager.UnloadSceneAsync(sceneData.SceneName);
        return result;
    }
}