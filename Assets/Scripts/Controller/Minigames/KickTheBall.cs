using System;
using System.Collections;
using UnityEngine;

public class KickTheBall : BaseMouseInputMinigameController
{
    [field: SerializeField]
    private Animator Animator { get; set; }

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

        Animator.Play("KickTheBall_Positive");
        MinigameSceneResult = new MinigameSceneResult(true, score, MinigameScene.Minigame.RemainingTime);
    }
}
