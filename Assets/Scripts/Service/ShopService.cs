using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ShopService : IService, IPreparable, IResettable
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
    
    public List<Modifier> GetCurrentShopItems()
    {
        var shopItems = new List<Modifier>();

        for (int i = 0; i < 3; i++)
        {
            

        }

        return shopItems;
    }
    
    public UniTask ResetSave()
    {
        return UniTask.CompletedTask;
    }
}
