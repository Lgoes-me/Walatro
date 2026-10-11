using System;

[Serializable]
public class ModifierInfo
{
    public string Id { get; set; }
    public Rarity Rarity { get; set; }

    protected bool Equals(ModifierInfo other)
    {
        return Id == other.Id && Rarity == other.Rarity;
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ModifierInfo)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, (int)Rarity);
    }
}