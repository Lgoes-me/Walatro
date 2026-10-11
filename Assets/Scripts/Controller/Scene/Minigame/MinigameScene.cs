using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MinigameScene : BaseAwaitableSceneWithResponse<MinigameSceneData, bool>
{
    [field: SerializeField] private Button WinButton { get; set; }
    [field: SerializeField] private Button GameOverButton { get; set; }
    [field: SerializeField] private Image ProgressBar { get; set; } 

    private SceneService SceneService { get; set; }
    private Coroutine CountdownCoroutine { get; set; }
    private int Timer { get; set; }

    protected override UniTask Init()
    {
        SceneService = Application.GetService<SceneService>();
        
        WinButton.onClick.RemoveAllListeners();
        WinButton.onClick.AddListener(() => Win().Forget());
        WinButton.Select();

        GameOverButton.onClick.RemoveAllListeners();
        GameOverButton.onClick.AddListener(() => GameOver().Forget());

        Timer = 3;

        CountdownCoroutine = StartCoroutine(CountdownEndMinigame());

        return UniTask.CompletedTask;
    }

    private UniTask Win()
    {
        CloseScene(true);
        return UniTask.CompletedTask;
    }
    
    private UniTask GameOver()
    {
        CloseScene(false);
        return UniTask.CompletedTask;
    }
    
    private IEnumerator CountdownEndMinigame()
    {
        yield return AnimateProgressBar();
        CloseScene(false);
    }

    public IEnumerator AnimateProgressBar()
    {
        var timer = 0f;

        while (timer <= Timer)
        {
            timer += Time.deltaTime;

            ProgressBar.fillAmount = 1 - timer / Timer;
            
            yield return new WaitForEndOfFrame();
        }
    }

    protected override void OnSceneClose(bool result)
    {
        if (CountdownCoroutine != null)
        {
            StopCoroutine(CountdownCoroutine);
            CountdownCoroutine = null;
        }
    }
}

public class MinigameSceneData : BaseSceneData
{
    public override string SceneName => "Minigame";
}