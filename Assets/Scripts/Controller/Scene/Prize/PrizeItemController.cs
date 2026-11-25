using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PrizeItemController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [field: SerializeField] private RawImage RawImage { get; set; }
    [field: SerializeField] private Button BuyButton { get; set; }
    
    private IPrize Prize { get; set; }

    public void Init(IPrize prize, Action action)
    {
        Prize = prize;

        RawImage.texture = Prize.Texture;
        
        BuyButton.onClick.RemoveAllListeners();
        BuyButton.onClick.AddListener(() => action?.Invoke());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Application.Instance.GetService<InterfaceService>().InfoDescriptor.Show(this.gameObject, Prize, Prize.Rarity);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Application.Instance.GetService<InterfaceService>().InfoDescriptor.Hide();
    }

    private void OnDisable()
    {
        Application.Instance.GetService<InterfaceService>().InfoDescriptor.Hide();
    }
}
