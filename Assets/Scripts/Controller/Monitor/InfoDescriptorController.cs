using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class InfoDescriptorController : MonoBehaviour
{
    [field: SerializeField] private RectTransform Pivot { get; set; }

    [field: SerializeField] private TMP_Text NameText { get; set; }
    [field: SerializeField] private TMP_Text DescriptionText { get; set; }

    public void Show(IInfoDescriptor info, Rarity rarity)
    {
        gameObject.SetActive(true);

        NameText.text = info.Name;
        DescriptionText.text = info.GetDescription(rarity);

        Pivot.anchoredPosition = Mouse.current.position.ReadValue();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
