using System;
using System.Collections;
using UnityEngine;

public abstract class BaseMinigameController : MonoBehaviour
{
    protected abstract Sprite Mouse { get; set; }

    protected MinigameSceneResult MinigameSceneResult { get; set; }

    protected MinigameScene MinigameScene { get; set; }
    
    private Action MouseReturn { get; set; }

    public void Init(MinigameScene minigameScene)
    {
        Application.Instance.GetService<InterfaceService>().MudarMouse(Mouse, 2);
        MinigameScene = minigameScene;
    }

    public abstract void ComputeMinigameInteraction(int score);

    public void CompleteAnimationCallback()
    {
        if (MinigameSceneResult == null)
            return;

        MinigameScene.EndMinigame(MinigameSceneResult);
    }

    private void OnDestroy()
    {
       Application.Instance.GetService<InterfaceService>().ResetMouse();
    }
}