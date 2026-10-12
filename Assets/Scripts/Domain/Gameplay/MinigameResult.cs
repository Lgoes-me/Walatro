using System.Collections.Generic;

public class MinigameResult
{
    public bool DidWin { get; set; }
    public int Score { get; set; }

    public MinigameResult(bool didWin)
    {
        DidWin = didWin;
        Score = didWin ? 100 : 0;
    }
}