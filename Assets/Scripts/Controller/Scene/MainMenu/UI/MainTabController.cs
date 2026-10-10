using UnityEngine;
using UnityEngine.UI;

public class MainTabController : MonoBehaviour
{
    [field: SerializeField] private Button ContinueButton { get; set; }
    [field: SerializeField] private Button StartButton { get; set; }
    [field: SerializeField] private Button SettingsButton { get; set; }
    [field: SerializeField] private Button ExitButton { get; set; }

    public void Init(GameSaveService gameSaveService, MainMenuScene scene)
    {
        gameObject.SetActive(true);

        ContinueButton.onClick.RemoveAllListeners();
        StartButton.onClick.RemoveAllListeners();
        SettingsButton.onClick.RemoveAllListeners();
        ExitButton.onClick.RemoveAllListeners();
        
        if (!gameSaveService.IsFirstTimePlaying())
        {
            ContinueButton.gameObject.SetActive(true);
            ContinueButton.onClick.AddListener(() => scene.ContinueGame().Forget());
            ContinueButton.Select();
            StartButton.onClick.AddListener(() => scene.StartGame().Forget());
        }
        else
        {
            ContinueButton.gameObject.SetActive(false);
            StartButton.onClick.AddListener(() => scene.StartGame().Forget());
            StartButton.Select();
        }
        
        SettingsButton.onClick.AddListener(scene.OpenSettingsTabController);
        ExitButton.onClick.AddListener(scene.ExitGame);
    }
}
