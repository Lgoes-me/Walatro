using System;
using System.Collections.Generic;

[Serializable]
public class GameBoxModel
{
    public List<GameDiscModel> Discs { get; set; }

    public GameBoxModel(List<GameDiscModel> discs)
    {
        Discs = discs;
    }

    protected bool Equals(GameBoxModel other)
    {
        return Discs == other.Discs;
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((GameBoxModel)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((List<GameDiscModel>)Discs);
    }
}