using System;
using Cysharp.Threading.Tasks;
using UnityEngine;


[Serializable]
public class AddScoreEffectData : IEffect
{
    [field: SerializeField] private RarityDefinedInt Score { get; set; }

    public async UniTask DoEffect(Rarity rarity) 
    {
        var gameplay = Application.Instance.GetComponent<GameplayService>().Gameplay;
    }

    public string[] GetTextParams(Rarity rarity)
    {
        return new[] { Score.GetValue(rarity).ToString() };
    }
}

