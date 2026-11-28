using UnityEngine;

[CreateAssetMenu]
public class PlanetData : PrizeData<Planet>
{
    public override Planet ToDomain()
    {
        return new Planet();
    }   
}