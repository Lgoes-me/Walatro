using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class BaseOverlayScene<T> : MonoBehaviour where T : BaseOverlaySceneData
{
    protected Application Application { get; private set; }
    protected T Data { get; private set; }

    public async UniTask OpenScene(Application application, T data)
    {
        Application = application;
        Data = data;
        
        await Init();
    }
    
    protected abstract UniTask Init();
}

public abstract class BaseOverlaySceneData
{
    public abstract string SceneName { get; }
}