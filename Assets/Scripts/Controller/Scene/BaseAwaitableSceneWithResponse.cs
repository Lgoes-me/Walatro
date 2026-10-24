using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class BaseAwaitableSceneWithResponse<T, T2> : BaseScene<T> where T : BaseSceneData
{
    private UniTaskCompletionSource<T2> TaskCompletionSource { get; set; }
    
    public UniTask<T2> WaitOpenScene(Application application, T data)
    {
        TaskCompletionSource = new UniTaskCompletionSource<T2>();
        OpenScene(application, data);
        return TaskCompletionSource.Task;
    }
    
    protected virtual void OnSceneClose(T2 result)
    {
        
    }

    protected void CloseScene(T2 result)
    {
        OnSceneClose(result);
        TaskCompletionSource.TrySetResult(result);
    }
}