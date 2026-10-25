public class Minigame
{
    public int TotalTimer { get; set; } = 3;
    public float CurrentTimer { get; set; }
    
    public float RemainingTime => TotalTimer - CurrentTimer;
}