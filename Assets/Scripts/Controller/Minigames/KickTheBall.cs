using System;
using System.Collections;
using UnityEngine;

public class KickTheBall : BaseMouseInputMinigameController
{
    [field: SerializeField]
    private Animator Animator { get; set; }

    [field: SerializeField]
    private ClickDetectorController BallClickDetector { get; set; }

    public override void Init(MinigameScene minigameScene, Gameplay gameplay)
    {
        base.Init(minigameScene, gameplay);

        BallClickDetector.Init(() => ScoreMinigame(50));
    }

    //Good score
    public override void ScoreMinigame(int points)
    {
        if (MinigameSceneResult != null)
            return;

        Animator.Play("KickTheBall_Positive");
        
        Gameplay.SetPoints(points);
        Gameplay.SetMult(MinigameScene.Minigame.RemainingTime);

        MinigameSceneResult = new MinigameSceneResult(true);
    }
}
