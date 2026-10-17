using System.Collections.Generic;
using UnityEngine;

public class Gameplay
{
    public List<Minigame> Minigames { get; set; }
    public int Vidas { get; set; }
    public int ScoreToBeat { get; set; }
    public int Score { get; set; }

    public Gameplay(int scoreToBeat)
    {
        Minigames = new List<Minigame>() {null, null, null, null, null};
        Vidas = 3;
        ScoreToBeat = scoreToBeat;

        Score = 0;
    }   
}