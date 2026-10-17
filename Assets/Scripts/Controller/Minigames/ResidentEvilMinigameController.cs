using System;
using UnityEngine;

public class ResidentEvilMinigameController : MonoBehaviour
{
    [field: SerializeField]
    private Sprite Mira { get; set; }

    [field: SerializeField]
    private GameObject MuzzleFlash { get; set; }

    private Action Callback { get; set; }

    void Start()
    {
        Callback = Application.Instance.GetService<InterfaceService>().MudarMousePorEnquanto(Mira);
    }

    void Update()
    {
        
        if(Input.GetMouseButtonDown(0))
        {
            MuzzleFlash.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        Callback();
    }
}
