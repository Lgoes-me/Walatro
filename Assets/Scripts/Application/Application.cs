using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Application : MonoBehaviour
{
    public static Application Instance { get; private set; }
    
    private Dictionary<Type, object> Services { get; set; }

    private List<KeyValuePair<int, List<IPreparable>>> Preparables { get; set; }
    private List<KeyValuePair<int, List<IBootable>>> Bootables { get; set; }
    private List<KeyValuePair<int, List<IStartable>>> Startables { get; set; }
    private List<KeyValuePair<int, List<IResettable>>> Resettables { get; set; }
    private List<KeyValuePair<int, List<IOnRoomEnter>>> OnRoomEnters { get; set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Init();
    }

    private void Init()
    {
        Services = new Dictionary<Type, object>();

        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsClass && !type.IsAbstract && typeof(IService).IsAssignableFrom(type))
                {
                    Register(type);
                }
            }
        }

        Preparables = Services.PrepareServices<IPreparable>("Prepare");
        Bootables = Services.PrepareServices<IBootable>("Boot");
        Startables = Services.PrepareServices<IStartable>("StartSave");
        Resettables = Services.PrepareServices<IResettable>("ResetSave");
        OnRoomEnters = Services.PrepareServices<IOnRoomEnter>("OnRoomEnter");
    }

    private void Start()
    {
        BootGame().Forget();
    }

    private async UniTaskVoid BootGame()
    {
        await PrepareServices();
        await BootServices();

        var gameSaveService = GetService<GameSaveService>();
        var sceneService = GetService<SceneService>();

        if (gameSaveService.IsFirstTimePlaying())
        {
            gameSaveService.StartNewSave();

            await StartServices();
            await sceneService.StartGame();
        }
        else
        {
            gameSaveService.LoadLastSave();

            await StartServices();
            await sceneService.WaitScene(new StartMenuSceneData());
        }
    }

    private async UniTask PrepareServices()
    {
        foreach (var (_, services) in Preparables)
        {
            var tasks = new List<UniTask>();

            foreach (var service in services)
            {
                tasks.Add(service.Prepare());
            }

            await UniTask.WhenAll(tasks);
        }
    }

    private async UniTask BootServices()
    {
        foreach (var (_, services) in Bootables)
        {
            var tasks = new List<UniTask>();

            foreach (var service in services)
            {
                tasks.Add(service.Boot());
            }
            
            await UniTask.WhenAll(tasks);
        }
    }

    public async UniTask StartServices()
    {
        foreach (var (_, services) in Startables)
        {
            var tasks = new List<UniTask>();

            foreach (var service in services)
            {
                tasks.Add(service.StartSave());
            }

            await UniTask.WhenAll(tasks);
        }
    }

    public async UniTask OnRoomEnter()
    {
        foreach (var (_, services) in OnRoomEnters)
        {
            var tasks = new List<UniTask>();

            foreach (var service in services)
            {
                tasks.Add(service.OnRoomEnter());
            }

            await UniTask.WhenAll(tasks);
        }
    }

    public async UniTask OnSaveReset()
    {
        foreach (var (_, services) in Resettables)
        {
            var tasks = new List<UniTask>();

            foreach (var service in services)
            {
                tasks.Add(service.ResetSave());
            }

            await UniTask.WhenAll(tasks);
        }
    }

    private void Register(Type serviceType)
    {
        var service = typeof(MonoBehaviour).IsAssignableFrom(serviceType)
            ? GetComponentInChildren(serviceType)
            : Activator.CreateInstance(serviceType);

        Services.Add(serviceType, service);
    }

    public T GetService<T>() where T : IService
    {
        return (T)Services[typeof(T)];
    }
}

public interface IService
{
}

//On Boot start
public interface IPreparable
{
    UniTask Prepare();
}

//After all Prepares
public interface IBootable
{
    UniTask Boot();
}

//After save is selected
public interface IStartable
{
    UniTask StartSave();
}

//On save reset
public interface IResettable
{
    UniTask ResetSave();
}

//After room loads
public interface IOnRoomEnter
{
    UniTask OnRoomEnter();
}

public class CallbackOrderAttribute : Attribute
{
    public int CallbackOrder { get; set; }

    public CallbackOrderAttribute()
    {
        CallbackOrder = 1;
    }

    public CallbackOrderAttribute(int callbackOrder)
    {
        CallbackOrder = callbackOrder;
    }
}