
public interface IInfoDescriptor
{
    string Name { get; }
    string GetDescription(Rarity rarity);
}