using System;
using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;
using Cysharp.Threading.Tasks;

public class InterfaceService: MonoBehaviour, IService, IBootable, IStartable, IOnGameOver
{
    [field: SerializeField]
    private Sprite MouseSprite { get; set; }

    [field: SerializeField]
    private Image Mouse { get; set; }

    [field: SerializeField]
    private Camera GameCamera { get; set; }

    [field: SerializeField]
    public CinemachineCamera CameraLonge { get; set; }

    [field: SerializeField]
    private CinemachineCamera CameraPerto { get; set; }

    [field: SerializeField]
    public InfoDescriptorController InfoDescriptor { get; set; }

    [field: SerializeField]
    private PrizeCanvasController PrizeCanvas { get; set; }

    [field: SerializeField]
    public ScreenMouseDetector ScreenMouseDetector { get; set; }

    [field: SerializeField] 
    public DecalManager DecalManager { get; set; }

    [field: SerializeField] 
    public GameDiscManager GameDiscManager { get; set; }

    [field: SerializeField] 
    public Transform HandPosition { get; set; }

    [field: SerializeField] 
    public Transform InsertPosition { get; set; }

    public UniTask Boot()
    {
        ResetMouse();
        CameraPerto.Priority = 1;
        CameraLonge.Priority = -1;
        return UniTask.CompletedTask;
    }

    public UniTask StartSave()
    {
        ResetMouse();
        return UniTask.CompletedTask;
    }

    public UniTask OnGameOver()
    {
        DecalManager.HideAll();
        return UniTask.CompletedTask;
    }

    public void MudarMouse(Sprite sprite)
    {
        if (Mouse == null)
            return;
            
        Mouse.sprite = sprite;
    }

    public async UniTask GivePrize()
    {
        await PrizeCanvas.Show();
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