using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PrizeItemController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [field: SerializeField] private RawImage RawImage { get; set; }
    
    private Modifier Modifier { get; set; }
    private Action Action { get; set; }

    public void Init(Modifier modifier, Action action)
    {
        Modifier = modifier;
        Action = action;

        RawImage.texture = Modifier.Texture;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Application.Instance.GetService<InterfaceService>().InfoDescriptor.Show(Modifier, Modifier.Rarity);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Application.Instance.GetService<InterfaceService>().InfoDescriptor.Hide();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Action?.Invoke();
    }

    private void OnDisable()
    {
        Application.Instance.GetService<InterfaceService>().InfoDescriptor.Hide();
    }
}
