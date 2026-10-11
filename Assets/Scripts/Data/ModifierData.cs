using UnityEngine;

[CreateAssetMenu]
public class ModifierData : PrizeData
{
    /*[field: SerializeReference] public BaseEventListener[] Events { get; private set; }
    [field: SerializeField] [field: TextArea(5,5)] private string Description { get; set; }
*/
    public Modifier ToDomain(ModifierInfo info)
    {
        return new Modifier();//(Id, name, Icon, GetRarity(info.Rarity), Events, Description);
    }
}