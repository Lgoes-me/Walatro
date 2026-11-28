using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlanetData : PrizeData<Planet>
{
    [field: SerializeField] public List<GameTag> GameTags { get; private set; }
    [field: SerializeField] public Texture Texture { get; private set; }
    [field: SerializeField] [field: TextArea(5,5)] private string Description { get; set; }

    public override Planet ToDomain()
    {
        return new Planet(Id, name, Texture, GameTags, Description);
    }   
}