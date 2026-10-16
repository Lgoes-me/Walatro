using System.Collections.Generic;
using UnityEngine;

public class Gameplay
{
    public List<int> Minigames { get; set; }
    public int Vidas { get; set; }
    public int ScoreToBeat { get; set; }

    public Gameplay(int scoreToBeat)
    {
        Minigames = new List<int>() {1, 2, 3, 4, 5};
        Vidas = 3;

        Debug.Log(scoreToBeat);
        ScoreToBeat = scoreToBeat;
    }
}