using System.Collections.Generic;
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
        AdditiveMult = 0;
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

    public void NextMinigame()
    {
        MinigameIndex ++;
    }

    public void SetVidas(int vidas)
    {
        Vidas = vidas;
        
        RaiseGameplayUpdatedEvent();
    }

    public void LoseVida()
    {
        Vidas --;
        
        RaiseGameplayUpdatedEvent();
    }

    public void GainVida()
    {
        Vidas ++;

        RaiseGameplayUpdatedEvent();
    }

    public void SetPoints(int points)
    {
        Points = points;
        
        RaiseGameplayUpdatedEvent();
    }

    public void SetMult(float mult)
    {
        Mult = mult;
        
        RaiseGameplayUpdatedEvent();
    }

    public void AddPoints(int points)
    {
        Points += points;
        
        RaiseGameplayUpdatedEvent();
    }

    public void AddMult(float mult)
    {
        AdditiveMult += mult;
        
        RaiseGameplayUpdatedEvent();
    }

    public void MultiplyMult(float mult)
    {
        MultiplicativeMult *= mult;
        
        RaiseGameplayUpdatedEvent();
    }

    public void SetScore()
    {
        Score = Mathf.CeilToInt(Points * Mult);
        Score *= MultiplicativeMult;

        Points = 0;
        AdditiveMult = 0;
        MultiplicativeMult = 1;
        
        RaiseGameplayUpdatedEvent();
    }

    public void AddScore(int score)
    {
        TotalScore += score;
        RaiseGameplayUpdatedEvent();
    }

    public void SetTotalScore()
    {
        TotalScore += Score;
        Score = 0;
        
        RaiseGameplayUpdatedEvent();
    }

    public virtual void RaiseGameplayUpdatedEvent()
    {
        OnGameplayUpdatedEvent?.Invoke();
    }
}