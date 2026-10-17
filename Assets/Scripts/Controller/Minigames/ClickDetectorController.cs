using UnityEngine;

public class ClickDetectorController : MonoBehaviour
{
    public void OnClick()
    {
        Debug.Log($"OnClick {name}");
    }
    
    public void OnMouseDown()
    {
        Debug.Log($"OnMouseDown {name}");
    }
}
