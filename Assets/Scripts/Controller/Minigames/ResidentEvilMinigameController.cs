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
        }
    }

    private void OnDestroy()
    {
        Callback();
    }

    private IEnumerator CoroutinaDeTiro()
    {
        MuzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        MuzzleFlash.SetActive(false);
    }
}
