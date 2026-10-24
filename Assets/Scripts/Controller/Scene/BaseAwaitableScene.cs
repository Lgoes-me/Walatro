using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class BaseAwaitableScene<T> : BaseScene<T> where T : BaseSceneData
{
    private UniTaskCompletionSource TaskCompletionSource { get; set; }
    
    public UniTask WaitOpenScene(Application application, T data)
    {
        TaskCompletionSource = new UniTaskCompletionSource();
        OpenScene(application, data);
        return TaskCompletionSource.Task;
    }
    
    protected virtual void OnSceneClose()
    {
        
    }

    protected void CloseScene()
    {
        OnSceneClose();
        TaskCompletionSource.TrySetResult();
    }
}