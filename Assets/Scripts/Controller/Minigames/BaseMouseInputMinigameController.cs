using System;
using System.Collections;
using UnityEngine;

public abstract class BaseMouseInputMinigameController : BaseMinigameController
{
    [field: SerializeField]
    protected override Sprite Mouse { get; set; }

    protected bool ClickEnabled { get; set; } = true;

    private void Update()
    {
        if(ClickEnabled && Input.GetMouseButtonDown(0))
        {
            ClickEnabled = false;
            OnClickInputReceived();

            var ray = Application.Instance.GetService<InterfaceService>().RaycastFromVirtualMouse();
            
            if (Physics.Raycast(ray, out var hit, 100f))
            {
                if (hit.collider.TryGetComponent<ClickDetectorController>(out var clickDetectorController))
                {
                    clickDetectorController.OnClick();
                }
            }
        }
    }

    protected virtual void OnClickInputReceived()
    {

    }
}
