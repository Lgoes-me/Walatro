using System.Collections.Generic;
using UnityEngine;

public class Planet : IPrize
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public Texture Texture { get; private set; }
    public List<GameTag> GameTags { get; private set; }
    private string Description { get; set; }

    public Planet(
        string id, 
        string name,
        Texture texture, 
        List<GameTag> gameTags,
        string description)
    {
        Id = id;
        Name = name;
        Texture = texture;
        GameTags = gameTags;
        Description = description;
    }

    public string GetDescription(Rarity rarity) => string.Format(Description ?? string.Empty, (this as IBaseEventData).GetTextParams(rarity));
}