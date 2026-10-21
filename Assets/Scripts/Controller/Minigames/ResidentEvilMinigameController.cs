using System;
using System.Collections;
using UnityEngine;

public class ResidentEvilMinigameController : MonoBehaviour
{
    [field: SerializeField]
    private Sprite Mira { get; set; }

    [field: SerializeField]
    private GameObject MuzzleFlash { get; set; }

    [field: SerializeField]
    private Animation Animation { get; set; }

    [field: SerializeField]
    private AnimationClip PositiveAnimationClip { get; set; }

    private Action<MinigameSceneResult> Callback { get; set; }
    private Action MouseReturn { get; set; }

    private MinigameSceneResult MinigameSceneResult { get; set; }
    
    private bool PodeAtirar { get; set; }

    public void Init(Action<MinigameSceneResult> callback)
    {
        MouseReturn = Application.Instance.GetService<InterfaceService>().MudarMousePorEnquanto(Mira, 2);
        PodeAtirar = true;
        Callback = callback;
    }

    private void Update()
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

    public void CompleteMinigame(int score)
    {
        Animation.AddClip(PositiveAnimationClip, "PositiveAnimationClip");
        Animation.Play("PositiveAnimationClip");
        MinigameSceneResult = new MinigameSceneResult(true, score);
    }

    public void CompleteCallback()
    {
        Callback(MinigameSceneResult);
    }

    private void OnDestroy()
    {
        MouseReturn();
    }

    private IEnumerator CoroutinaDeTiro()
    {
        MuzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        MuzzleFlash.SetActive(false);
    }
}
