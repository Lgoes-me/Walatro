using UnityEngine;

[CreateAssetMenu]
public class ItemData : PrizeData<Item>
{
    public override Item ToDomain()
    {
        return new Item();
    }
}