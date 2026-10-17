using System;
using System.Collections;
using UnityEngine;

public class ResidentEvilMinigameController : MonoBehaviour
{
    [field: SerializeField]
    private Sprite Mira { get; set; }

    [field: SerializeField]
    private GameObject MuzzleFlash { get; set; }

    private Action Callback { get; set; }
    private bool PodeAtirar { get; set; }

    void Start()
    {
        Callback = Application.Instance.GetService<InterfaceService>().MudarMousePorEnquanto(Mira, 2);
        PodeAtirar = true;
    }

    void Update()
    {
        if(PodeAtirar && Input.GetMouseButtonDown(0))
        {
            PodeAtirar = false;
            StartCoroutine(CoroutinaDeTiro());

            var ray = Application.Instance.GetService<InterfaceService>().RaycastFromVirtualMouse();
            
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green);
            
            if (Physics.Raycast(ray, out var hit, 100f))
            {
                if (hit.collider.TryGetComponent<ClickDetectorController>(out var clickDetectorController))
                {
                    clickDetectorController.OnClick();
                }
            }
        }
    }

    private void OnDestroy()
    {
        Callback();
    }

    private IEnumerator CoroutinaDeTiro()
    {
        MuzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        MuzzleFlash.SetActive(false);
    }
}
