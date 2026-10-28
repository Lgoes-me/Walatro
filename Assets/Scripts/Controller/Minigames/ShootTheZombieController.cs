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
    
    [field: SerializeField]
    private ClickDetectorController HeadClickDetector { get; set; }

    [field: SerializeField]
    private ClickDetectorController BodyClickDetector { get; set; }

    private bool PodeAtirar { get; set; } = true;

    public override void Init(MinigameScene minigameScene)
    {
        base.Init(minigameScene);

        HeadClickDetector.Init(() => ComputeMinigameInteraction(50));
        BodyClickDetector.Init(() => ComputeMinigameInteraction(20));
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

    public override void ComputeMinigameInteraction(int score)
    {
        if (MinigameSceneResult != null)
            return;

        Animation.AddClip(PositiveAnimationClip, "PositiveAnimationClip");
        Animation.Play("PositiveAnimationClip");
        MinigameSceneResult = new MinigameSceneResult(true, score, MinigameScene.Minigame.RemainingTime);
    }

    public void TerminateMinigame()
    {
        if (MinigameSceneResult != null)
            return;
            
        MinigameSceneResult = new MinigameSceneResult(false, 0, 0);
    }

    private IEnumerator CoroutinaDeTiro()
    {
        MuzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        MuzzleFlash.SetActive(false);
    }
}
