public interface IEffect : IComponentData
{
    string[] GetTextParams(Rarity rarity);
}