using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PrizeItemController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [field: SerializeField] private Button BuyButton { get; set; }
    
    private Modifier Modifier { get; set; }

    public void Init(Modifier modifier, Action action)
    {
        Modifier = modifier;
        
        BuyButton.onClick.RemoveAllListeners();
        BuyButton.onClick.AddListener(() => action?.Invoke());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Application.Instance.GetService<InterfaceService>().InfoDescriptor.Show(Modifier, Modifier.Rarity);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Application.Instance.GetService<InterfaceService>().InfoDescriptor.Hide();
    }
}
