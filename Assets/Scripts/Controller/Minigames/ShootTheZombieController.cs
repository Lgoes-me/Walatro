using System;
using System.Collections;
using UnityEngine;

public class ShootTheZombieController : BaseMouseInputMinigameController
{
    [field: SerializeField]
    private GameObject MuzzleFlash { get; set; }

    [field: SerializeField]
    private Animator Animator { get; set; }
    
    [field: SerializeField]
    private ClickDetectorController HeadClickDetector { get; set; }

    [field: SerializeField]
    private ClickDetectorController BodyClickDetector { get; set; }

    public override void Init(MinigameScene minigameScene, Gameplay gameplay)
    {
        base.Init(minigameScene, gameplay);

        HeadClickDetector.Init(() => ScoreMinigame(50));
        BodyClickDetector.Init(() => ScoreMinigame(20));
    }

    protected override void OnClickInputReceived()
    {
        ClickEnabled = false;
        StartCoroutine(CoroutinaDeTiro());
    }

    //Good score
    public override void ScoreMinigame(int points)
    {
        if (MinigameSceneResult != null)
            return;

        Animator.Play("ShootTheZombie_Positive");

        Gameplay.SetPoints(points);
        Gameplay.SetMult(MinigameScene.Minigame.RemainingTime);

        MinigameSceneResult = new MinigameSceneResult(true);
    }

    private IEnumerator CoroutinaDeTiro()
    {
        MuzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        MuzzleFlash.SetActive(false);
    }
}
