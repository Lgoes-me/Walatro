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
        
        await Init();
    }
    
    protected abstract UniTask Init();
}

public abstract class BaseSceneData
{
    public abstract string SceneName { get; }
}