using System;
using System.Collections;
using UnityEngine;

public abstract class BaseMinigameController : MonoBehaviour
{
    protected abstract Sprite Mouse { get; set; }

    protected MinigameSceneResult MinigameSceneResult { get; set; }

    protected Action<MinigameSceneResult> Callback { get; set; }
    
    private Action MouseReturn { get; set; }

    public void Init(Action<MinigameSceneResult> callback)
    {
        MouseReturn = Application.Instance.GetService<InterfaceService>().MudarMousePorEnquanto(Mouse, 2);
        Callback = callback;
    }

    public abstract void ComputeMinigameInteraction(int score);

    public virtual void CompleteAnimationCallback()
    {
        if (MinigameSceneResult == null)
            return;

        Callback(MinigameSceneResult);
    }

    private void OnDestroy()
    {
        MouseReturn();
    }
}