using System;

[Serializable]
public class ModifierModel
{
    public string Id { get; set; }
    public Rarity Rarity { get; set; }

    public ModifierModel(
        string id,
        Rarity rarity)
    {
        Id = id;
        Rarity = rarity;
    }

    protected bool Equals(ModifierModel other)
    {
        return Id == other.Id && Rarity == other.Rarity;
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ModifierModel)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, (int)Rarity);
    }
}