using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class TutorialService : IService , IPreparable, IBootable
{
    private Tutorial Tutorial { get; set; }
    private SaveManager SaveManager { get; set; }
    private Dictionary<string, TutorialData> GameTutorialsDict { get; set; }
    
    public UniTask Prepare()
    {
        SaveManager = new SaveManager();
        return UniTask.CompletedTask;
    }

    public UniTask Boot()
    {
        Tutorial = SaveManager.LoadData<Tutorial>() ?? new Tutorial();
        
        GameTutorialsDict = new List<TutorialData>()
        {
            new SimpleTutorialData("GameStart"),
            new SimpleTutorialData("Gameplay"),
            new SimpleTutorialData("Lives"),
            new SimpleTutorialData("Money"),
            new SimpleTutorialData("LivesReset"),
            new SimpleTutorialData("Shop"),
            new SimpleTutorialData("Boss"),
            new SimpleTutorialData("GameOver"),
        }.ToIdentifiableDictionary();
        
        return UniTask.CompletedTask;
    }

    public bool HasFinishedTutorial()
    {
        return Tutorial.Shown.Contains("GameOver");
    }

    public async UniTask TryShowTutorial(string id, ITutorialFocus focus)
    {
        if (Tutorial.Shown.Contains(id))
            return;

        if (!GameTutorialsDict.TryGetValue(id, out TutorialData tutorial) || !tutorial.CanShow())
            return;
        
        await focus.ShowTutorial(tutorial);
        
        Tutorial.Shown.Add(id);
    }
}