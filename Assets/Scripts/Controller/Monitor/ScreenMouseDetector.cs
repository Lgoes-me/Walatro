using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class ScreenMouseDetector : MonoBehaviour
{
    public InputSystemUIInputModule worldInput;
    
    public VirtualInputModule gameInput;

    public RectTransform canvasRect;
    
    public RectTransform uiIndicator;
    
    public Camera Camera;

    public bool Enabled;
    
    private void Awake()
    {
        Enabled = true;
    }

    private void OnMouseOver()
    {
        if (!Enabled)
            return;

        Cursor.visible = false;
        uiIndicator.gameObject.SetActive(true);

        worldInput.enabled = false;
        gameInput.enabled = true;
    }

    private void Update()
    {
        if (!Enabled)
            return;

        Ray ray = Camera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out var hit))
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                Vector2 uvCoords = hit.textureCoord;
                Vector2 canvasSize = canvasRect.sizeDelta;

                Vector2 anchoredPos = new Vector2(
                    (uvCoords.x - canvasRect.pivot.x) * canvasSize.x,
                    (uvCoords.y - canvasRect.pivot.y) * canvasSize.y
                );

                uiIndicator.anchoredPosition = anchoredPos;
            }
        }
    }

    private void OnMouseExit()
    {
        if (!Enabled)
            return;

        Cursor.visible = true;
        uiIndicator.gameObject.SetActive(false);
        
        worldInput.enabled = true;
        gameInput.enabled = false;
    }

    public void Disable()
    {
        OnMouseExit();
        Enabled = false;
    }
    
    public void Enable()
    {
        Enabled = true;
    }
}
