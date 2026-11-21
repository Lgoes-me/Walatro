using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

[Serializable]
public class AddMultEffectData : IGameplayStartEffect , IMinigameEndEffect
{
    [field: SerializeField] private RarityDefinedFloat Mult { get; set; }

    public AddMultEffectData(RarityDefinedFloat mult)
    {
        Mult = mult;
    }

    public async UniTask DoEffect(Rarity rarity) 
    {
        var mult = Mult.GetValue(rarity);
        await Application.Instance.GetService<GameplayService>().Gameplay.AddMult(mult);
    }

    public string[] GetTextParams(Rarity rarity)
    {
        return new[] { Mult.GetValue(rarity).ToString() };
    }
}

[Serializable]
public class MultiplyMultEffectData : IMinigameEndEffect
{
    [field: SerializeField] private RarityDefinedFloat Mult { get; set; }

    public MultiplyMultEffectData(RarityDefinedFloat mult)
    {
        Mult = mult;
    }

    public async UniTask DoEffect(Rarity rarity) 
    {
        var mult = Mult.GetValue(rarity);
        await Application.Instance.GetService<GameplayService>().Gameplay.MultiplyMult(mult);
    }

    public string[] GetTextParams(Rarity rarity)
    {
        return new[] { Mult.GetValue(rarity).ToString() };
    }
}