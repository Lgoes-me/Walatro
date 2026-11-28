using UnityEngine;

[CreateAssetMenu]
public class TarotData : PrizeData<Tarot>
{
    public override Tarot ToDomain()
    {
        return new Tarot();
    }   
}