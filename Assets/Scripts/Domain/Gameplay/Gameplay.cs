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
    
    public delegate void GameplayValueChangedEventHandler<T>(T value);
    public event GameplayValueChangedEventHandler<int> OnPointsAddedEvent;
    public event GameplayValueChangedEventHandler<float> OnMultAddedEvent;
    public event GameplayValueChangedEventHandler<float> OnMultMultipliedEvent;
    public event GameplayValueChangedEventHandler<int> OnScoreAddedEvent;

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

    public UniTask NextMinigame()
    {
        MinigameIndex ++;
        return UniTask.CompletedTask;
    }

    public void SetVidas(int vidas)
    {
        Vidas = vidas;
    }

    public async UniTask LoseVida()
    {
        Vidas --;
        
        OnGameplayUpdatedEvent?.Invoke();
        await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);
    }

    public async UniTask GainVida()
    {
        Vidas ++;

        OnGameplayUpdatedEvent?.Invoke();
        await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);
    }

    public void SetPoints(int points)
    {
        Points = points;
        OnGameplayUpdatedEvent?.Invoke();
    }

    public void SetMult(float mult)
    {
        AdditiveMult = mult;
        OnGameplayUpdatedEvent?.Invoke();
    }

    public async UniTask AddPoints(int points)
    {
        OnPointsAddedEvent?.Invoke(points);

        await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);

        Points += points;
        
        OnGameplayUpdatedEvent?.Invoke();

        await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);
    }

    public async UniTask AddMult(float mult)
    {
        OnMultAddedEvent?.Invoke(mult);

        await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);

        AdditiveMult += mult;
        
        OnGameplayUpdatedEvent?.Invoke();

        await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);
    }

    public async UniTask MultiplyMult(float mult)
    {
        OnMultMultipliedEvent?.Invoke(mult);

        await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);

        MultiplicativeMult *= mult;
        
        OnGameplayUpdatedEvent?.Invoke();

        await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);
    }

    public async UniTask SetScore()
    {
        Score = Mathf.CeilToInt(Points * AdditiveMult * MultiplicativeMult);

        Points = 0;
        AdditiveMult = 1;
        MultiplicativeMult = 1;
        
        OnGameplayUpdatedEvent?.Invoke();

        await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);
    }

    public async UniTask AddScore(int score)
    {
        OnScoreAddedEvent?.Invoke(score);

        await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);

        TotalScore += score;
        
        OnGameplayUpdatedEvent?.Invoke();

        await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);
    }

    public async UniTask SetTotalScore()
    {
        TotalScore += Score;

        Score = 0;
        
        OnGameplayUpdatedEvent?.Invoke();

        await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);
    }
}