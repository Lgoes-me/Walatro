using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PrizeManager : MonoBehaviour
{
    [field: SerializeField] private List<PrizeItemController> Items { get; set; }
    [field: SerializeField] private WorldButtonController RerollButton { get; set; }

    private GameSaveService GameSaveService { get; set; }
    private PrizeService PrizeService { get; set; }
    private InterfaceService InterfaceService { get; set; }

    private List<IPrize> Prizes { get; set; }

    public async UniTask Show()
    {
        GameSaveService = Application.Instance.GetService<GameSaveService>();
        PrizeService = Application.Instance.GetService<PrizeService>();
        InterfaceService = Application.Instance.GetService<InterfaceService>();
        
        Prizes = await PrizeService.GetCurrentPrizeItems(); 

        InterfaceService.ScreenMouseDetector.Disable();

        gameObject.SetActive(true);

        Items[0].Init(Prizes[0], () => SelectItem(0));
        Items[1].Init(Prizes[1], () => SelectItem(1));
        Items[2].Init(Prizes[2], () => SelectItem(2));
        Items[3].Init(Prizes[3], () => SelectItem(3));
        Items[4].Init(Prizes[4], () => SelectItem(4));
        Items[5].Init(Prizes[5], () => SelectItem(5));

        RerollButton.Init(() => RerollItems().Forget());

        //Items[3].gameObject.SetActive(false);
        //Items[4].gameObject.SetActive(false);
        //Items[5].gameObject.SetActive(false);
        RerollButton.gameObject.SetActive(false);
    }
    
    private void SelectItem(int i)
    {
        //feedback visual
        PrizeService.AddPrize(Prizes[i]);
        
        //desativa outros botoes
        gameObject.SetActive(false);
        InterfaceService.ScreenMouseDetector.Enable();
    }

    private async UniTask RerollItems()
    {
        //load desativando botões
        Prizes = await PrizeService.GetCurrentPrizeItems(); 
        //rerenderiza botoes
    }
}
