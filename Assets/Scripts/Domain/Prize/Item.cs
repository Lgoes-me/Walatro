using UnityEngine;
using Cysharp.Threading.Tasks;

public class Item : IPrize
{
    public string Name { get; private set; }
    public Texture Texture { get; private set; }
    public Rarity Rarity { get; private set; }
    private string Description { get; set; }

    public Item()
    {
    }

    public string GetDescription(Rarity rarity) => string.Format(Description ?? string.Empty, (this as IBaseEventData).GetTextParams(rarity));
}