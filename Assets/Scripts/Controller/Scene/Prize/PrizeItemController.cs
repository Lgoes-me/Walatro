using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PrizeItemController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [field: SerializeField] private WorldButtonController Button { get; set; }
    
    private GameObject Child { get; set; }
    private IPrize Prize { get; set; }
    private IInfoDescriptor InfoDescriptor { get; set; }

    public void Init(IPrize prize, Action action)
    {
        Prize = prize;
        InfoDescriptor = prize as IInfoDescriptor;

        Button.Init(() => action?.Invoke());

        if (Child != null)
        {
            Destroy(Child);
        }
        
        Child = Application.Instance.GetService<PrizeService>().InstantiateButton(Prize, this.transform);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (InfoDescriptor == null)
            return;

        Application.Instance.GetService<InterfaceService>().InfoDescriptor.Show(this.gameObject, InfoDescriptor, InfoDescriptor.Rarity);
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
