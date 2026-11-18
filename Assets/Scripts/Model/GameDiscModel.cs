using System;

[Serializable]
public class GameDiscModel
{
    public string Id { get; set; }
    public DiscModifier Modifier { get; set; }
    
    public GameDiscModel(string id, DiscModifier modifier)
    {
        Id = id;
        Modifier = modifier;
    }

    protected bool Equals(GameDiscModel other)
    {
        return Id == other.Id && Modifier == other.Modifier;
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((GameDiscModel)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Modifier);
    }
}