using System;
using System.Collections;
using UnityEngine;

public class ShootTheZombieController : BaseMouseInputMinigameController
{
    [field: SerializeField]
    private GameObject MuzzleFlash { get; set; }

    [field: SerializeField]
    private Animation Animation { get; set; }

    [field: SerializeField]
    private AnimationClip PositiveAnimationClip { get; set; }
    
    [field: SerializeField]
    private ClickDetectorController HeadClickDetector { get; set; }

    [field: SerializeField]
    private ClickDetectorController BodyClickDetector { get; set; }

    public override void Init(MinigameScene minigameScene)
    {
        base.Init(minigameScene);

        HeadClickDetector.Init(() => ScoreMinigame(50));
        BodyClickDetector.Init(() => ScoreMinigame(20));
    }

    protected override void OnClickInputReceived()
    {
        StartCoroutine(CoroutinaDeTiro());
    }

    //Good score
    public override void ScoreMinigame(int score)
    {
        if (MinigameSceneResult != null)
            return;

        Animation.AddClip(PositiveAnimationClip, "PositiveAnimationClip");
        Animation.Play("PositiveAnimationClip");
        MinigameSceneResult = new MinigameSceneResult(true, score, MinigameScene.Minigame.RemainingTime);
    }

    private IEnumerator CoroutinaDeTiro()
    {
        MuzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        MuzzleFlash.SetActive(false);
    }
}
