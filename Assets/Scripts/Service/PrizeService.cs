using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PrizeService : IService, IPreparable, IResettable
{
    private GameSaveService GameSaveService { get; set; }
    private RandomService RandomService { get; set; }
    private ContentService ContentService { get; set; }

    public UniTask Prepare()
    {
        var application = Application.Instance;
        GameSaveService = application.GetService<GameSaveService>();
        RandomService = application.GetService<RandomService>();
        ContentService = application.GetService<ContentService>();

        return UniTask.CompletedTask;
    }
    
    public async UniTask<List<Modifier>> GetCurrentPrizeItems()
    {
        var shopItems = new List<Modifier>();

        for (int i = 0; i < 4; i++)
        {
            var modifierDataAsset = RandomService.RandomElement(ContentService.Modifiers);
            var modifierData = await modifierDataAsset.LoadAssetAsyncIfValid();
            var modifierModel = new ModifierModel(modifierData.Id, RandomService.RandomRarity());
            
            var modifier = modifierData.ToDomain(modifierModel);

            shopItems.Add(modifier);
        }

        return shopItems;
    }
    
    public UniTask ResetSave()
    {
        return UniTask.CompletedTask;
    }
}
