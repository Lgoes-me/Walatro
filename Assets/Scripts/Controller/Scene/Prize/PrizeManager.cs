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

        var quantidade = 3;

        for (int i = 0; i < Items.Count; i ++)
        {
            var item = Items[i];
            
            item.Init(Prizes[i], () => SelectItem(i));
            item.transform.position = new Vector3(-8 + ((16/((float)quantidade - 1)) * i), item.transform.position.y, item.transform.position.z);

            if(i >= quantidade)
            {
                item.gameObject.SetActive(false);
            }
        }

        gameObject.SetActive(true);

        RerollButton.Init(() => RerollItems().Forget());

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
