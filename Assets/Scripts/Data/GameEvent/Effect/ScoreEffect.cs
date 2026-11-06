using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

[Serializable]
public class AddScoreEffectData : IGameplayStartEffect , IMinigameEndEffect
{
    [field: SerializeField] private RarityDefinedInt Score { get; set; }

    public async UniTask DoEffect(Rarity rarity) 
    {
        Application.Instance.GetService<GameplayService>().Gameplay.AddScore(Score.GetValue(rarity));
    }

    public string[] GetTextParams(Rarity rarity)
    {
        return new[] { Score.GetValue(rarity).ToString() };
    }
}

