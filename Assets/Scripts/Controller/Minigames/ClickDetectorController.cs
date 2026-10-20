using UnityEngine;

public class ClickDetectorController : MonoBehaviour
{
    [field: SerializeField]
    private int AreaScore { get; set; }

    [field: SerializeField]
    private ResidentEvilMinigameController MinigameController { get; set; }

    public void OnClick()
    {
        MinigameController.CompleteMinigame(AreaScore);
    }
}
