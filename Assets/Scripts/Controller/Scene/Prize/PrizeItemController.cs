using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrizeItemController : MonoBehaviour
{
    [field: SerializeField] private Button BuyButton { get; set; }
    [field: SerializeField] private TMP_Text NameText { get; set; }
    [field: SerializeField] private TMP_Text DescriptionText { get; set; }

    public void Init(Modifier modifier, Action action)
    {
        BuyButton.onClick.RemoveAllListeners();
        BuyButton.onClick.AddListener(() => action?.Invoke());

        NameText.text = modifier.Name;
        DescriptionText.text = modifier.GetDescription(modifier.Rarity);
    }
}
