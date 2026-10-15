using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class BaseAwaitableSceneWithResponse<T, T2> : MonoBehaviour where T : BaseSceneData
{
    protected Application Application { get; private set; }
    protected T Data { get; private set; }
    
    private UniTaskCompletionSource<T2> TaskCompletionSource { get; set; }
    
    public UniTask<T2> WaitOpenScene(Application application, T data)
    {
        Application = application;
        Data = data;
        TaskCompletionSource = new UniTaskCompletionSource<T2>();
        Init().Forget();

        var canvases = Object.FindObjectsByType<Canvas>();

        foreach(var canvas in canvases)
        {
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = Application.MainCamera;
        }

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