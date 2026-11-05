using System;
using System.Collections;
using UnityEngine;

public abstract class BaseMinigameController : MonoBehaviour
{
    protected abstract Sprite Mouse { get; set; }

    protected MinigameSceneResult MinigameSceneResult { get; set; }

    protected MinigameScene MinigameScene { get; set; }

    protected Gameplay Gameplay { get; set; }
    
    private Action MouseReturn { get; set; }

    public virtual void Init(MinigameScene minigameScene)
    {
        MinigameScene = minigameScene;

        Application.Instance.GetService<InterfaceService>().MudarMouse(Mouse);
        Gameplay = Application.Instance.GetService<GameplayService>().Gameplay;
    }

    public void CompleteAnimationCallback()
    {
        if (MinigameSceneResult == null)
            return;

        MinigameScene.EndMinigame(MinigameSceneResult);
    }

    public abstract void ScoreMinigame(int points);
    
    public void FailMinigame()
    {
        if (MinigameSceneResult != null)
            return;
            
        MinigameSceneResult = new MinigameSceneResult(false);
    }

    private void OnDestroy()
    {
       Application.Instance.GetService<InterfaceService>().ResetMouse();
    }
}