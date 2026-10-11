using Cysharp.Threading.Tasks;

public abstract class TutorialData : IIdentifiable
{
    public string Id { get; set; }

    protected TutorialData(string id)
    {
        Id = id;
    }

    public abstract bool CanShow();
}


public interface ITutorialFocus
{
    UniTask ShowTutorial(TutorialData tutorialData);
}