using System;
using System.Collections;
using UnityEngine;

public abstract class BaseMinigameController : MonoBehaviour
{
    protected abstract Sprite Mouse { get; set; }

    protected MinigameSceneResult MinigameSceneResult { get; set; }

    protected MinigameScene MinigameScene { get; set; }
    
    private Action MouseReturn { get; set; }

    public virtual void Init(MinigameScene minigameScene)
    {
        Application.Instance.GetService<InterfaceService>().MudarMouse(Mouse);
        MinigameScene = minigameScene;
    }

    public void CompleteAnimationCallback()
    {
        if (MinigameSceneResult == null)
            return;

        MinigameScene.EndMinigame(MinigameSceneResult);
    }

    public abstract void ScoreMinigame(int score);
    
    public void FailMinigame()
    {
        if (MinigameSceneResult != null)
            return;
            
        MinigameSceneResult = new MinigameSceneResult(false, 0, 0);
    }

    private void OnDestroy()
    {
       Application.Instance.GetService<InterfaceService>().ResetMouse();
    }
}