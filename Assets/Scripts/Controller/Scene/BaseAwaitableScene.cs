using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class BaseAwaitableScene<T> : MonoBehaviour where T : BaseSceneData
{
    [field: SerializeField] protected int CanvasDistance { get; set; } = 100;
    [field: SerializeField] protected Canvas Canvas { get; set; }

    protected Application Application { get; private set; }
    protected T Data { get; private set; }
    
    private UniTaskCompletionSource TaskCompletionSource { get; set; }
    
    public UniTask WaitOpenScene(Application application, T data)
    {
        Application = application;
        Data = data;
        TaskCompletionSource = new UniTaskCompletionSource();
        Init().Forget();
        
        Canvas.renderMode = RenderMode.ScreenSpaceCamera;
        Canvas.worldCamera = Application.MainCamera;
        Canvas.planeDistance = CanvasDistance;

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