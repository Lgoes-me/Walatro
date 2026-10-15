using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class BaseAwaitableScene<T> : MonoBehaviour where T : BaseSceneData
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
        
        var canvases = Object.FindObjectsByType<Canvas>();

        foreach(var canvas in canvases)
        {
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = Application.MainCamera;
        }
        
        return TaskCompletionSource.Task;
    }

    protected abstract UniTask Init();
    
    protected virtual void OnSceneClose()
    {
        
    }

    protected void CloseScene()
    {
        OnSceneClose();
        TaskCompletionSource.TrySetResult();
    }
}