using Cysharp.Threading.Tasks;

public class SimpleTutorialData : TutorialData
{
    public SimpleTutorialData(string id) : base(id)
    {
        
    }

    public override bool CanShow()
    {
        return true;
    }
}