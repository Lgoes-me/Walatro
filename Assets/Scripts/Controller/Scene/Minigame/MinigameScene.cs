using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MinigameScene : BaseAwaitableSceneWithResponse<MinigameSceneData, MinigameSceneResult>
{
    [field: SerializeField] private Image ProgressBar { get; set; }

    private SceneService SceneService { get; set; }
    private BaseMinigameController MinigameController { get; set; }
    private Coroutine CountdownCoroutine { get; set; }
    private int Timer { get; set; }

    protected override UniTask Init()
    {
        SceneService = Application.GetService<SceneService>();

        Timer = 3;

        CountdownCoroutine = StartCoroutine(CountdownEndMinigame());

        MinigameController = Instantiate(Data.GameDisc.MinigameControllerPrefab);
        MinigameController.Init(EndMinigame);

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

    private void OnDestroy()
    {
        Destroy(MinigameController.gameObject);
    }
}

public class MinigameSceneData : BaseSceneData
{
    public override string SceneName => "Minigame";
    public GameDisc GameDisc { get; set; }

    public MinigameSceneData(GameDisc gameDisc)
    {
        GameDisc = gameDisc;
    }
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