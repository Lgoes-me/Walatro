using System;
using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;
using Cysharp.Threading.Tasks;

public class EnvironmentService: MonoBehaviour, IService, IOnGameOver
{
    [field: SerializeField]
    private PrizeCanvasController PrizeCanvas { get; set; }

    [field: SerializeField] 
    public DecalManager DecalManager { get; set; }

    [field: SerializeField] 
    public GameDiscManager GameDiscManager { get; set; }

    [field: SerializeField] 
    public Transform HandPosition { get; set; }

    [field: SerializeField] 
    public Transform InsertPosition { get; set; }

    [field: SerializeField] 
    public Transform DiscardPosition { get; set; }

    public UniTask OnGameOver()
    {
        DecalManager.HideAll();
        return UniTask.CompletedTask;
    }

    public async UniTask GivePrize()
    {
        await PrizeCanvas.Show();
    }
}