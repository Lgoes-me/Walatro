using System.Collections.Generic;

public class Gameplay
{
    public List<int> Minigames { get; set; }
    public int Vidas { get; set; }
    public int ScoreToBeat { get; set; }

    public Gameplay()
    {
        Minigames = new List<int>() {1, 2, 3, 4, 5};
        Vidas = 3;
        ScoreToBeat = 500;
    }
}