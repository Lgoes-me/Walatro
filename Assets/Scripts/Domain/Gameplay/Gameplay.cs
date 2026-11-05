using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Gameplay
{
    public List<Minigame> Minigames { get; private set; }
    
    public int MinigameIndex { get; private set; }

    public int Vidas { get; private set; }
    
    public int Points { get; private set; }
    
    public float AdditiveMult { get; private set; }

    public float MultiplicativeMult { get; private set; }

    public int Score { get; private set; }

    public int TotalScore { get; private set; }

    public int ScoreToBeat { get; private set; }
    
    public delegate void GameplayUpdateEventHandler();

    public event GameplayUpdateEventHandler OnGameplayUpdatedEvent;

    public Gameplay(int scoreToBeat)
    {
        Minigames = new List<Minigame>();
        MinigameIndex = 0;

        ScoreToBeat = scoreToBeat;

        Vidas = 0;
        Points = 0;
        AdditiveMult = 1;
        MultiplicativeMult = 1;
        Score = 0;
        TotalScore = 0;
    }   

    public void SetMinigames(int games)
    {
        for(int i = 0; i < games; i++)
        {
            Minigames.Add(null);
        }
    }

    public async UniTask NextMinigame()
    {
        MinigameIndex ++;
    }

    public async UniTask SetVidas(int vidas)
    {
        Vidas = vidas;
        
        await RaiseGameplayUpdatedEvent();
    }

    public async UniTask LoseVida()
    {
        Vidas --;
        
        await RaiseGameplayUpdatedEvent();
    }

    public async UniTask GainVida()
    {
        Vidas ++;

        await RaiseGameplayUpdatedEvent();
    }

    public async UniTask SetPoints(int points)
    {
        Points = points;
        
        await RaiseGameplayUpdatedEvent();
    }

    public async UniTask SetMult(float mult)
    {
        AdditiveMult = mult;
        
        await RaiseGameplayUpdatedEvent();
    }

    public async UniTask AddPoints(int points)
    {
        Points += points;
        
        await RaiseGameplayUpdatedEvent();
    }

    public async UniTask AddMult(float mult)
    {
        AdditiveMult += mult;
        
        await RaiseGameplayUpdatedEvent();
    }

    public async UniTask MultiplyMult(float mult)
    {
        MultiplicativeMult *= mult;
        
        await RaiseGameplayUpdatedEvent();
    }

    public async UniTask SetScore()
    {
        Score = Mathf.CeilToInt(Points * AdditiveMult * MultiplicativeMult);

        Points = 0;
        AdditiveMult = 1;
        MultiplicativeMult = 1;
        
        await RaiseGameplayUpdatedEvent();
    }

    public async UniTask AddScore(int score)
    {
        TotalScore += score;
        await RaiseGameplayUpdatedEvent();
    }

    public async UniTask SetTotalScore()
    {
        TotalScore += Score;
        Score = 0;
        
        await RaiseGameplayUpdatedEvent();
    }

    public async UniTask RaiseGameplayUpdatedEvent()
    {
        OnGameplayUpdatedEvent?.Invoke();
        
        await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);
    }
}