using System;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceService: MonoBehaviour, IService
{
    [field: SerializeField]
    private Image Mouse { get; set; }

    [field: SerializeField]
    private Camera GameCamera { get; set; }

    public Action MudarMousePorEnquanto(Sprite sprite, float size)
    {
        var oldMouse = Mouse.sprite;
        Mouse.sprite = sprite;
        Mouse.transform.localScale = size * Vector3.one;

        return () => 
        {
            Mouse.sprite = oldMouse;
            Mouse.transform.localScale = Vector3.one;
        };
    }

    public Ray RaycastFromVirtualMouse()
    {
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(GameCamera, Mouse.transform.position);
        return GameCamera.ScreenPointToRay(screenPoint);
    }
}