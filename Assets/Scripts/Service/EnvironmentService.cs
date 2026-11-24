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

    public UniTask OnGameOver()
    {
        GameDiscManager.HideAll();
        DecalManager.HideAll();
        return UniTask.CompletedTask;
    }

    public async UniTask GivePrize()
    {
        await PrizeCanvas.Show();
    }
}