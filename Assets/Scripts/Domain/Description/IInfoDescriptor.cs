
public interface IInfoDescriptor
{
    string Name { get; }
    Rarity Rarity { get; }
    string GetDescription(Rarity rarity);
}