using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MinigameScene : BaseAwaitableSceneWithResponse<MinigameSceneData, MinigameSceneResult>
{
    [field: SerializeField] private Image ProgressBar { get; set; }

    public Minigame Minigame { get; set; }

    private SceneService SceneService { get; set; }

    private BaseMinigameController MinigameController { get; set; }

    private Coroutine CountdownCoroutine { get; set; }

    protected override void Init()
    {
        Minigame = new Minigame();

        SceneService = Application.GetService<SceneService>();

        MinigameController = Instantiate(Data.GameDisc.MinigameControllerPrefab);
        MinigameController.Init(this);

        CountdownCoroutine = StartCoroutine(CountdownEndMinigame());
    }

    public void EndMinigame(MinigameSceneResult result)
    {
        CloseScene(result);
    }

    private IEnumerator CountdownEndMinigame()
    {
        yield return AnimateProgressBar();
    }

    public IEnumerator AnimateProgressBar()
    {
        Minigame.CurrentTimer = 0f;

        while (Minigame.CurrentTimer <= Minigame.TotalTimer)
        {
            Minigame.CurrentTimer += Time.deltaTime;

            ProgressBar.fillAmount = 1 - Minigame.CurrentTimer / Minigame.TotalTimer;
            
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
    public float Multiplier { get; set; }

    public MinigameSceneResult(bool didWin, int score, float multiplier)
    {
        DidWin = didWin;
        Score = score;
        Multiplier = multiplier;
    }
}