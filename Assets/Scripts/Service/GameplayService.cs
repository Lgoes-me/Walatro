using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class GameplayService : IService, IPreparable, IStartable
{
    public List<int> Levels = new List<int>()
    {
        100, 
        800,
        2000,
        5000,
        11000,
        20000,
        35000,
        50000,
    };

    public int CurrentLevel { get; set; }

    private GameSaveService GameSaveService { get; set; }

    public UniTask Prepare()
    {
        var application = Application.Instance;
        GameSaveService = application.GetService<GameSaveService>();

        return UniTask.CompletedTask;
    }

    public UniTask StartSave()
    {
        CurrentLevel = GameSaveService.Save.CurrentLevel;
        return UniTask.CompletedTask;
    }
}