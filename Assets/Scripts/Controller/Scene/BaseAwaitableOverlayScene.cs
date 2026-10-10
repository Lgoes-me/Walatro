using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class BaseAwaitableOverlayScene<T> : MonoBehaviour where T : BaseOverlaySceneData
{
    protected Application Application { get; private set; }
    protected T Data { get; private set; }
    
    private UniTaskCompletionSource TaskCompletionSource { get; set; }
    
    public UniTask WaitOpenScene(Application application, T data)
    {
        Application = application;
        Data = data;
        TaskCompletionSource = new UniTaskCompletionSource();
        Init().Forget();
        
        return TaskCompletionSource.Task;
    }

    protected abstract UniTask Init();
    
    protected void CloseScene()
    {
        TaskCompletionSource.TrySetResult();
    }
}