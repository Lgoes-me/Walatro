using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class PrizeData : ScriptableObject, IIdentifiable
{
    [field: SerializeField] public string Id { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public List<Rarity> AvailableRarities { get; private set; }

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

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!string.IsNullOrEmpty(Id))
            return;

        if (AssetDatabase.TryGetGUIDAndLocalFileIdentifier(this, out var guid, out _))
        {
            Id = guid;
        }
    }
#endif
}