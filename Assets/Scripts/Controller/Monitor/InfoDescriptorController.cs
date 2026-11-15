using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class InfoDescriptorController : MonoBehaviour
{
    [field: SerializeField] private RectTransform Pivot { get; set; }
    [field: SerializeField] private TMP_Text NameText { get; set; }
    [field: SerializeField] private TMP_Text DescriptionText { get; set; }
    [field: SerializeField] private RectTransform Canvas { get; set; }
    [field: SerializeField] private Camera Camera { get; set; }

    public void Show(GameObject focus, IInfoDescriptor info, Rarity rarity)
    {
        gameObject.SetActive(true);

        NameText.text = info.Name;
        DescriptionText.text = info.GetDescription(rarity);
        
        if (focus.transform is RectTransform rectTransform)
        {
            Pivot.anchoredPosition = rectTransform.position;
        }
        else
        {
            Pivot.anchoredPosition = Camera.WorldToScreenPoint(focus.transform.position) - Canvas.position;
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
