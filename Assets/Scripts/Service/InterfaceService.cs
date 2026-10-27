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
    private CinemachineCamera CameraLonge { get; set; }

    [field: SerializeField]
    private CinemachineCamera CameraMedia { get; set; }

    [field: SerializeField]
    private CinemachineCamera CameraPerto { get; set; }


    public UniTask Boot()
    {
        ResetMouse();
        CameraMedia.Priority = 1;
        CameraLonge.Priority = -1;
        return UniTask.CompletedTask;
    }

    public void StartGame()
    {
        CameraPerto.Priority = 1;
        CameraMedia.Priority = -1;
    }

    public UniTask StartSave()
    {
        ResetMouse();
        return UniTask.CompletedTask;
    }

    public void MudarMouse(Sprite sprite, float size)
    {
        if (Mouse == null)
            return;
            
        Mouse.sprite = sprite;
        Mouse.transform.localScale = size * Vector3.one;
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
        Mouse.transform.localScale = Vector3.one;
    }
}