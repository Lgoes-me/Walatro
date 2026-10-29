using System;
using System.Collections;
using UnityEngine;

public class KickTheBall : BaseMouseInputMinigameController
{
    [field: SerializeField]
    private Animation Animation { get; set; }

    [field: SerializeField]
    private AnimationClip PositiveAnimationClip { get; set; }

    [field: SerializeField]
    private ClickDetectorController BallClickDetector { get; set; }

    public override void Init(MinigameScene minigameScene)
    {
        base.Init(minigameScene);

        BallClickDetector.Init(() => ScoreMinigame(50));
    }

    //Good score
    public override void ScoreMinigame(int score)
    {
        if (MinigameSceneResult != null)
            return;

        Animation.AddClip(PositiveAnimationClip, "PositiveAnimationClip");
        Animation.Play("PositiveAnimationClip", PlayMode.StopAll);
        MinigameSceneResult = new MinigameSceneResult(true, score, MinigameScene.Minigame.RemainingTime);
    }
}
