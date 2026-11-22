using System;
using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;
using Cysharp.Threading.Tasks;

public class CameraService: MonoBehaviour, IService, IBootable
{
    [field: SerializeField]
    private CinemachineCamera CameraLonge { get; set; }

    [field: SerializeField]
    private CinemachineCamera CameraPerto { get; set; }

    public UniTask Boot()
    {
        CameraPerto.Priority = 1;
        CameraLonge.Priority = -1;
        return UniTask.CompletedTask;
    }
}