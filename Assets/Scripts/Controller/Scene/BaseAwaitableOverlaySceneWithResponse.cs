using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class BaseAwaitableOverlaySceneWithResponse<T, T2> : MonoBehaviour where T : BaseOverlaySceneData
{
    protected T Data { get; private set; }
    
    private UniTaskCompletionSource<T2> TaskCompletionSource { get; set; }
    
    public UniTask<T2> WaitOpenScene(Application application, T data)
    {
        Data = data;
        TaskCompletionSource = new UniTaskCompletionSource<T2>();
        Init().Forget();

        return TaskCompletionSource.Task;
    }

    protected abstract UniTaskVoid Init();
    
    protected void CloseScene(T2 result)
    {
        TaskCompletionSource.TrySetResult(result);
    }
}