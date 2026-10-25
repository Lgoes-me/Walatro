using UnityEngine;

[CreateAssetMenu]
public class ModifierData : PrizeData
{
    [field: SerializeField] public Material Material { get; private set; }

    [field: SerializeReference] public BaseEventListener[] Events { get; private set; }
    
    [field: SerializeField] [field: TextArea(5,5)] private string Description { get; set; }

    public Modifier ToDomain(ModifierModel model)
    {
        return new Modifier(Id, name, Icon, GetRarity(model.Rarity), Events, Description);
    }
}