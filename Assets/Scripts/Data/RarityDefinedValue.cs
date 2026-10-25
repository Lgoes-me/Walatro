using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class RarityDefinedInt
{
    [field: SerializeField]
    private Dictionary<Rarity, int> Possibilities { get; set; }

    public RarityDefinedInt(Dictionary<Rarity, int> possibilities)
    {
        Possibilities = new Dictionary<Rarity, int>(possibilities);
    }
    
    public int GetValue(Rarity rarity)
    {
        if (Possibilities.TryGetValue(rarity, out var value))
        {
            return value;
        }

        return Possibilities.GetValueOrDefault(Rarity.Deactivated);
    }
}

[Serializable]
public class RarityDefinedFloat
{
    [field: SerializeField]
    private Dictionary<Rarity, float> Possibilities { get; set; }

    public RarityDefinedFloat(Dictionary<Rarity, float> possibilities)
    {
        Possibilities = new Dictionary<Rarity, float>(possibilities);
    }
    
    public float GetValue(Rarity rarity)
    {
        if (Possibilities.TryGetValue(rarity, out var value))
        {
            return value;
        }

        return Possibilities.GetValueOrDefault(Rarity.Deactivated);
    }
}