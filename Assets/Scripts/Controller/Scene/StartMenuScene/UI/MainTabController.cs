using UnityEngine;
using UnityEngine.UI;

public class MainTabController : MonoBehaviour
{
    [field: SerializeField] private Button ContinueButton { get; set; }
    [field: SerializeField] private Button StartButton { get; set; }
    [field: SerializeField] private Button SettingsButton { get; set; }
    [field: SerializeField] private Button ExitButton { get; set; }

    public void Init(GameSaveService gameSaveService, StartMenuScene startMenuScene)
    {
        gameObject.SetActive(true);

        ContinueButton.onClick.RemoveAllListeners();
        StartButton.onClick.RemoveAllListeners();
        SettingsButton.onClick.RemoveAllListeners();
        ExitButton.onClick.RemoveAllListeners();
        
        if (!gameSaveService.IsFirstTimePlaying())
        {
            ContinueButton.gameObject.SetActive(true);
            ContinueButton.onClick.AddListener(() => startMenuScene.ContinueGame().Forget());
            ContinueButton.Select();
            StartButton.onClick.AddListener(() => startMenuScene.StartGame().Forget());
        }
        else
        {
            ContinueButton.gameObject.SetActive(false);
            StartButton.onClick.AddListener(() => startMenuScene.StartGame().Forget());
            StartButton.Select();
        }
        
        SettingsButton.onClick.AddListener(startMenuScene.OpenSettingsTabController);
        ExitButton.onClick.AddListener(startMenuScene.ExitGame);
    }
}
