using System;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceService: MonoBehaviour, IService
{
    [field: SerializeField]
    private Image Mouse { get; set; }

    public Action MudarMousePorEnquanto(Sprite sprite)
    {
        var oldMouse = Mouse.sprite;
        Mouse.sprite = sprite;

        return () => 
        {
            Mouse.sprite = oldMouse;
        };

    }

}