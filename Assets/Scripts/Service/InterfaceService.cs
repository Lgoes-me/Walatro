using System;
using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;
using Cysharp.Threading.Tasks;

public class InterfaceService: MonoBehaviour, IService, IBootable, IStartable
{
    [field: SerializeField]
    private Sprite MouseSprite { get; set; }

    [field: SerializeField]
    private Image Mouse { get; set; }

    [field: SerializeField]
    private Camera GameCamera { get; set; }

    [field: SerializeField]
    public InfoDescriptorController InfoDescriptor { get; set; }

    [field: SerializeField]
    public ScreenMouseDetector ScreenMouseDetector { get; set; }

    public UniTask Boot()
    {
        ResetMouse();
        return UniTask.CompletedTask;
    }

    public UniTask StartSave()
    {
        ResetMouse();
        return UniTask.CompletedTask;
    }

    public void MudarMouse(Sprite sprite)
    {
        if (Mouse == null)
            return;
            
        Mouse.sprite = sprite;
    }

    public Ray RaycastFromVirtualMouse()
    {
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(GameCamera, Mouse.transform.position);
        return GameCamera.ScreenPointToRay(screenPoint);
    }

    public void ResetMouse()
    {
        if (Mouse == null)
            return;

        Mouse.sprite = MouseSprite;
    }
}