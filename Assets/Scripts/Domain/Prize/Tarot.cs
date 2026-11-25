using UnityEngine;
using Cysharp.Threading.Tasks;

public class Tarot : IPrize
{
    public string Name { get; private set; }
    public Texture Texture { get; private set; }
    public Rarity Rarity { get; private set; }
    private string Description { get; set; }

    public Tarot(
        string name,
        Texture texture, 
        Rarity rarity, 
        string description)
    {
        Name = name;
        Texture = texture;
        Rarity = rarity;
        Description = description;
    }

    public string GetDescription(Rarity rarity) => string.Format(Description ?? string.Empty, (this as IBaseEventData).GetTextParams(rarity));
}