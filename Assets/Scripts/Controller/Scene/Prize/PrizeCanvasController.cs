using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PrizeCanvasController : MonoBehaviour
{
    [field: SerializeField] private List<PrizeItemController> Items { get; set; }
    [field: SerializeField] private Button RerollButton { get; set; }

    private GameSaveService GameSaveService { get; set; }
    private ModifierService ModifierService { get; set; }
    private PrizeService PrizeService { get; set; }
    private InterfaceService InterfaceService { get; set; }

    private List<Modifier> Modifiers { get; set; }

    public async void Show()
    {
        GameSaveService = Application.Instance.GetService<GameSaveService>();
        ModifierService = Application.Instance.GetService<ModifierService>();
        PrizeService = Application.Instance.GetService<PrizeService>();
        InterfaceService = Application.Instance.GetService<InterfaceService>();
        
        Modifiers = await PrizeService.GetCurrentPrizeItems(); 

        InterfaceService.ScreenMouseDetector.Disable();

        gameObject.SetActive(true);

        Items[0].Init(Modifiers[0], () => SelectItem(0).Forget());
        Items[1].Init(Modifiers[1], () => SelectItem(1).Forget());
        Items[2].Init(Modifiers[2], () => SelectItem(2).Forget());
        Items[3].Init(Modifiers[3], () => SelectItem(3).Forget());

        RerollButton.onClick.RemoveAllListeners();
        RerollButton.onClick.AddListener(() => RerollItems().Forget());

        Items[3].gameObject.SetActive(false);
        RerollButton.gameObject.SetActive(false);
    }
    
    private async UniTask SelectItem(int i)
    {
        //feedback visual
        ModifierService.AddModifier(Modifiers[i]);
        //desativa outros botoes
        gameObject.SetActive(false);
        InterfaceService.ScreenMouseDetector.Enable();
    }

    private async UniTask RerollItems()
    {
        //load desativando botões
        Modifiers = await PrizeService.GetCurrentPrizeItems(); 
        //rerenderiza botoes
    }
}
