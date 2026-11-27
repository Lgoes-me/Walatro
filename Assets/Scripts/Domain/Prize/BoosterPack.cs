using System.Collections.Generic;

public class BoosterPack
{
    public List<IPrize> Prizes { get; set; }
    public int Size { get; set; }
    public PrizeType PrizeType { get; set; }
    public bool HasReroll { get; set; }
    
    public BoosterPack(int size, PrizeType prizeType, bool hasReroll)
    {
        Prizes = new List<IPrize>();
        Size = size;
        PrizeType = prizeType;
        HasReroll = hasReroll;
    }

    public void SetPrizes(List<IPrize> prizes)
    {
        Prizes = prizes;
    }
}

public enum PrizeType
{
    Modifier,
    Item,
    GameDisc,
    Tarot,
    Planet
}