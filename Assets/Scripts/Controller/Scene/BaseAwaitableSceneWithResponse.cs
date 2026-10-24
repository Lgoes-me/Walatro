using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class BaseAwaitableSceneWithResponse<T, T2> : MonoBehaviour where T : BaseSceneData
{
    [field: SerializeField] protected int CanvasDistance { get; set; } = 100;
    [field: SerializeField] protected Canvas Canvas { get; set; }

    protected Application Application { get; private set; }
    protected T Data { get; private set; }
    
    private UniTaskCompletionSource<T2> TaskCompletionSource { get; set; }
    
    public UniTask<T2> WaitOpenScene(Application application, T data)
    {
        Application = application;
        Data = data;
        TaskCompletionSource = new UniTaskCompletionSource<T2>();
        Init().Forget();

        Canvas.renderMode = RenderMode.ScreenSpaceCamera;
        Canvas.worldCamera = Application.MainCamera;
        Canvas.planeDistance = CanvasDistance;

        return TaskCompletionSource.Task;
    }

    protected abstract UniTask Init();
    
    protected virtual void OnSceneClose(T2 result)
    {
        
    }

    protected void CloseScene(T2 result)
    {
        OnSceneClose(result);
        TaskCompletionSource.TrySetResult(result);
    }
}