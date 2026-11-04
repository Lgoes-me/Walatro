using System.Collections.Generic;
using UnityEngine;

public class Gameplay
{
    public List<Minigame> Minigames { get; private set; }
    
    public int MinigameIndex { get; private set; }

    public int Vidas { get; private set; }
    
    public int Points { get; private set; }
    
    public float Mult { get; private set; }

    public int Score { get; private set; }

    public int ScoreToBeat { get; private set; }
    
    public delegate void GameplayUpdateEventHandler();

    public event GameplayUpdateEventHandler OnGameplayUpdatedEvent;

    public Gameplay(int scoreToBeat)
    {
        Minigames = new List<Minigame>() {null, null, null, null, null};
        MinigameIndex = 0;
        Vidas = 3;
        ScoreToBeat = scoreToBeat;

        Points = 0;
        Mult = 0;
        Score = 0;
    }   

    public void SetMinigames()
    {
        
    }

    public void NextMinigame()
    {

    }

    public void SetVidas(int vidas)
    {
        Vidas = vidas;
    }

    public void LoseVida()
    {
        Vidas --;
    }

    public void GainVida()
    {
        Vidas ++;
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
        Mult += mult;
        
        RaiseGameplayUpdatedEvent();
    }

    public void MultiplyMult(float mult)
    {
        Mult *= mult;
        
        RaiseGameplayUpdatedEvent();
    }

    public void SetScore()
    {
        Score += Mathf.CeilToInt(Points * Mult);

        Points = 0;
        Mult = 0;
        
        RaiseGameplayUpdatedEvent();
    }

    public void AddScore(int score)
    {
        Score += score;

        RaiseGameplayUpdatedEvent();
    }

    public virtual void RaiseGameplayUpdatedEvent()
    {
        OnGameplayUpdatedEvent?.Invoke();
    }
}