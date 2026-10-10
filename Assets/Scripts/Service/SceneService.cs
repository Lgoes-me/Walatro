using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneService : MonoBehaviour, IService, IPreparable
{
    [field: SerializeField] private CanvasGroup Transition { get; set; }

    private Application Application { get; set; }

    public UniTask Prepare()
    {
        Application = Application.Instance;
        return UniTask.CompletedTask;
    }

    public async UniTask WaitScene<T>(T sceneData) where T : BaseSceneData
    {
        await SceneManager.LoadSceneAsync(sceneData.SceneName, LoadSceneMode.Additive);
        await FindAnyObjectByType<BaseAwaitableScene<T>>().WaitOpenScene(Application, sceneData);
        await SceneManager.UnloadSceneAsync(sceneData.SceneName);
    }

    public async UniTask<T2> WaitSceneWithResponse<T, T2>(T sceneData) where T : BaseSceneData
    {
        await SceneManager.LoadSceneAsync(sceneData.SceneName, LoadSceneMode.Additive);
        var result = await FindAnyObjectByType<BaseAwaitableSceneWithResponse<T, T2>>().WaitOpenScene(Application, sceneData);
        await SceneManager.UnloadSceneAsync(sceneData.SceneName);
        return result;
    }

    public async UniTaskVoid SwitchScene<T>(T sceneData) where T : BaseSceneData
    {
        var scene = SceneManager.GetSceneByName(sceneData.SceneName);

        if (scene.IsValid() && scene.isLoaded)
        {
            await SceneManager.UnloadSceneAsync(scene);
        }
        else
        {
            await SceneManager.LoadSceneAsync(sceneData.SceneName, LoadSceneMode.Additive);
            await FindAnyObjectByType<BaseScene<T>>().OpenScene(Application, sceneData);
        }
    }

    public async UniTask LoadGameplayScene()
    {
        var scene = SceneManager.GetSceneByName("RoomScene");

        if (scene.IsValid() && scene.isLoaded)
        {
            return;
        }

        await SceneManager.LoadSceneAsync("RoomScene", LoadSceneMode.Single);
    }

    public async UniTask StartTransitionAnimation()
    {
        Transition.alpha = 0;
        await Transition.DOFade(1, 0.1f).ToUniTask();
    }

    public async UniTask CompleteTransitionAnimation()
    {
        Transition.alpha = 1;
        await Transition.DOFade(0, 0.1f).ToUniTask();
        Transition.gameObject.SetActive(false);
    }
}