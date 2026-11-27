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

    private BoosterPack BoosterPack { get; set; }

    public async UniTask Show()
    {
        GameSaveService = Application.Instance.GetService<GameSaveService>();
        PrizeService = Application.Instance.GetService<PrizeService>();
        InterfaceService = Application.Instance.GetService<InterfaceService>();

        InterfaceService.ScreenMouseDetector.Disable();
        
        BoosterPack = await PrizeService.CreateBoosterPack();

        for (int i = 0; i < Items.Count; i++)
        {
            var index = i;

            var item = Items[index];
            item.Init(BoosterPack.Prizes[index], () => SelectItem(index));
            item.transform.position = new Vector3(-8 + ((16/(float)(BoosterPack.Size - 1)) * index), item.transform.position.y, item.transform.position.z);

            if(index >= BoosterPack.Size)
            {
                item.gameObject.SetActive(false);
            }
        }

        RerollButton.Init(() => RerollItems().Forget());
        RerollButton.gameObject.SetActive(BoosterPack.HasReroll);

        gameObject.SetActive(true);
    }
    
    private void SelectItem(int i)
    {
        //feedback visual
        PrizeService.AddPrize(BoosterPack.Prizes[i]);
        
        //desativa outros botoes
        gameObject.SetActive(false);
        InterfaceService.ScreenMouseDetector.Enable();
    }

    private async UniTask RerollItems()
    {
        //load desativando botões
        var prizes = await PrizeService.GetPrizeItems(BoosterPack.PrizeType); 
        BoosterPack.SetPrizes(prizes);
        //rerenderiza botoes
    }
}
