using Cysharp.Threading.Tasks;

public class GameplayService : IService, IStartable
{
    public UniTask StartSave()
    {
        return UniTask.CompletedTask;
    }
}