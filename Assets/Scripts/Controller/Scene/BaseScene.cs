using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class BaseScene<T> : MonoBehaviour where T : BaseSceneData
{
    [field: SerializeField] protected int CanvasDistance { get; set; } = 100;
    [field: SerializeField] protected Canvas Canvas { get; set; }

    protected Application Application { get; private set; }
    protected T Data { get; private set; }

    public async UniTask OpenScene(Application application, T data)
    {
        Application = application;
        Data = data;
        
        Canvas.renderMode = RenderMode.ScreenSpaceCamera;
        Canvas.worldCamera = Application.MainCamera;
        Canvas.planeDistance = CanvasDistance;
        
        await Init();
    }
    
    protected abstract UniTask Init();
}

public abstract class BaseSceneData
{
    public abstract string SceneName { get; }
}