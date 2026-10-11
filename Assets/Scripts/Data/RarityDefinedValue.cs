using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class RarityDefinedValue<T>
{
    protected Dictionary<Rarity, T> Possibilities { get; private set; }

    protected RarityDefinedValue(Dictionary<Rarity, T> possibilities)
    {
        Possibilities = new Dictionary<Rarity, T>(possibilities);
    }

    public T GetValue(Rarity rarity)
    {
        if (Possibilities.TryGetValue(rarity, out var value))
        {
            return value;
        }

        return Possibilities.GetValueOrDefault(Rarity.Deactivated);
    }
}

[Serializable]
public class RarityDefinedInt : RarityDefinedValue<int>
{
    public RarityDefinedInt(Dictionary<Rarity, int> possibilities) : base(possibilities)
    {
    }
}

[Serializable]
public class RarityDefinedFloat : RarityDefinedValue<float>
{
    public RarityDefinedFloat(Dictionary<Rarity, float> possibilities) : base(possibilities)
    {
    }
}