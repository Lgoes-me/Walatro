using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

[Serializable]
public class AddPointsEffectData : IGameplayStartEffect , IMinigameEndEffect
{
    [field: SerializeField] private RarityDefinedInt Points { get; set; }

    public AddPointsEffectData(RarityDefinedInt points)
    {
        Points = points;
    }

    public async UniTask DoEffect(Rarity rarity) 
    {
        var points = Points.GetValue(rarity);
        await Application.Instance.GetService<GameplayService>().Gameplay.AddPoints(points);
    }

    public string[] GetTextParams(Rarity rarity)
    {
        return new[] { Points.GetValue(rarity).ToString() };
    }
}