using System;
using System.Collections;
using UnityEngine;

public class ShootTheZombieController : BaseMinigameController
{
    [field: SerializeField]
    protected override Sprite Mouse { get; set; }

    [field: SerializeField]
    private GameObject MuzzleFlash { get; set; }

    [field: SerializeField]
    private Animation Animation { get; set; }

    [field: SerializeField]
    private AnimationClip PositiveAnimationClip { get; set; }
    
    private bool PodeAtirar { get; set; } = true;

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

    public override void ComputeMinigameInteraction(int score)
    {
        Animation.AddClip(PositiveAnimationClip, "PositiveAnimationClip");
        Animation.Play("PositiveAnimationClip");
        MinigameSceneResult = new MinigameSceneResult(true, score);
    }

    public override void CompleteAnimationCallback()
    {
        Callback(MinigameSceneResult);
    }

    private IEnumerator CoroutinaDeTiro()
    {
        MuzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        MuzzleFlash.SetActive(false);
    }
}
