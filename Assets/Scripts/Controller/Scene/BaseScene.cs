using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class BaseScene<T> : MonoBehaviour where T : BaseSceneData
{
    protected Application Application { get; private set; }
    protected T Data { get; private set; }

    public async UniTask OpenScene(Application application, T data)
    {
        Application = application;
        Data = data;

        var canvases = Object.FindObjectsByType<Canvas>();

        foreach(var canvas in canvases)
        {
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = Application.MainCamera;
        }
        
        await Init();
    }
    
    protected abstract UniTask Init();
}

public abstract class BaseSceneData
{
    public abstract string SceneName { get; }
}