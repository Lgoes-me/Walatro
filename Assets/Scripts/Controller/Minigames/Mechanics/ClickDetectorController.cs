using System;
using UnityEngine;

public class ClickDetectorController : MonoBehaviour
{
    private Action OnClickAction { get; set; }

    public void Init(Action onClickAction)
    {
        OnClickAction = onClickAction;
    }

    public void OnClick()
    {
        OnClickAction?.Invoke();
    }
}
