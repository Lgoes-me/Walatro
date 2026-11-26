using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldButtonController : MonoBehaviour, IPointerClickHandler
{
    private Action Click { get; set; }

    public void Init(Action click)
    {
        Click = click;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Click?.Invoke();
    }
}