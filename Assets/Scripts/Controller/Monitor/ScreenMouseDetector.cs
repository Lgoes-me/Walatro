using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenMouseDetector : MonoBehaviour
{
    
    public RectTransform canvasRect;
    
    public RectTransform uiIndicator;
    
    public Camera Camera;
    
    void OnMouseOver()
    {
        Cursor.visible = false;
        uiIndicator.gameObject.SetActive(true);
    }

    private void Update()
    {
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

    void OnMouseExit()
    {
        Cursor.visible = true;
        uiIndicator.gameObject.SetActive(false);
    }
}
