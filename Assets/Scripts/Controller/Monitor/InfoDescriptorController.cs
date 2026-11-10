using UnityEngine;
using TMPro;

public class InfoDescriptorController : MonoBehaviour
{
    [field: SerializeField] private TMP_Text NameText { get; set; }
    [field: SerializeField] private TMP_Text DescriptionText { get; set; }

    public void Show(IInfoDescriptor info, Rarity rarity)
    {
        gameObject.SetActive(true);
        NameText.text = info.Name;
        DescriptionText.text = info.GetDescription(rarity);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
