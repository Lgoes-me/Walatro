using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MinigameScene : BaseAwaitableSceneWithResponse<MinigameSceneData, MinigameSceneResult>
{
    [field: SerializeField] private Image ProgressBar { get; set; } 
    [field: SerializeField] private ResidentEvilMinigameController ResidentEvilMinigameController { get; set; } 

    private SceneService SceneService { get; set; }
    private Coroutine CountdownCoroutine { get; set; }
    private int Timer { get; set; }

    protected override UniTask Init()
    {
        SceneService = Application.GetService<SceneService>();

        Timer = 3;

        CountdownCoroutine = StartCoroutine(CountdownEndMinigame());

        ResidentEvilMinigameController.Init(EndMinigame);

        return UniTask.CompletedTask;
    }

    private void EndMinigame(MinigameSceneResult result)
    {
        CloseScene(result);
    }

    private IEnumerator CountdownEndMinigame()
    {
        yield return AnimateProgressBar();
        CloseScene(new MinigameSceneResult(false, 0));
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

    protected override void OnSceneClose(MinigameSceneResult result)
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

public class MinigameSceneResult
{
    public bool DidWin { get; set; }
    public int Score { get; set; }

    public MinigameSceneResult(bool didWin, int score)
    {
        DidWin = didWin;
        Score = score;
    }
}