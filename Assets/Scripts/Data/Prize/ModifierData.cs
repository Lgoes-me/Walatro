using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ModifierData : PrizeData<Modifier>
{
    [field: SerializeField] public List<Rarity> AvailableRarities { get; private set; }

    [field: SerializeField] public Texture Texture { get; private set; }

    [field: SerializeReference] public BaseEventListener[] Events { get; private set; }
    
    [field: SerializeField] [field: TextArea(5,5)] private string Description { get; set; }

    public Modifier ToDomain(ModifierModel model)
    {
        return new Modifier(Id, name, Texture, GetRarity(model.Rarity), Events, Description);
    }

    public override Modifier ToDomain()
    {
        return new Modifier(Id, name, Texture, Application.Instance.GetService<RandomService>().RandomRarity(), Events, Description);
    }

    protected Rarity GetRarity(Rarity rarity)
    {
        //primeiro verifica para baixo a raridade disponivel
        
        for (int i = (int)rarity; i >= (int)Rarity.Common; i--)
        {
            if (!AvailableRarities.Contains((Rarity) i))
                continue;

            return (Rarity) i;
        }
        
        //se não houver pega a primeira maior que encontrar

        for (int i = (int)rarity; i <= (int)Rarity.Legendary; i++)
        {
            if (!AvailableRarities.Contains((Rarity) i))
                continue;

            return (Rarity) i;
        }

        return Rarity.Deactivated;
    }
}