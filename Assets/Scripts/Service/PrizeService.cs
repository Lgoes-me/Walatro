using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PrizeService : IService, IPreparable
{
    private GameSaveService GameSaveService { get; set; }
    private RandomService RandomService { get; set; }
    private ContentService ContentService { get; set; }
    private ModifierService ModifierService { get; set; }

    public UniTask Prepare()
    {
        var application = Application.Instance;
        GameSaveService = application.GetService<GameSaveService>();
        RandomService = application.GetService<RandomService>();
        ContentService = application.GetService<ContentService>();
        ModifierService = application.GetService<ModifierService>();

        return UniTask.CompletedTask;
    }
    
    public async UniTask<List<IPrize>> GetCurrentPrizeItems()
    {
        return await GetModifiers();
        return await GetItems();
        return await GetTarots();
        return await GetPlanets();
    }

    public void AddPrize(IPrize prize)
    {
        switch (prize)
        {
            case null:
                break;

            case Modifier modifier:
                ModifierService.AddModifier(modifier);
                break;

            case Item item:
                
                break;

            case Tarot tarot:
                
                break;
                
            case Planet planet:
                
                break;
                
            default:
                break;
        }

    }

    public async UniTask<List<IPrize>> GetModifiers()
    {
        var modifiers = new List<IPrize>();

        for (int i = 0; i < 4; i++)
        {
            var modifierDataAsset = RandomService.RandomElement(ContentService.Modifiers);
            var modifierData = await modifierDataAsset.LoadAssetAsyncIfValid();
            var modifierModel = new ModifierModel(modifierData.Id, RandomService.RandomRarity());
            
            var modifier = modifierData.ToDomain(modifierModel);

            modifiers.Add(modifier);
        }

        return modifiers;
    }

    public async UniTask<List<IPrize>> GetItems()
    {
        var items = new List<IPrize>();
        return items;
    }

    public async UniTask<List<IPrize>> GetTarots()
    {
        var tarots = new List<IPrize>();
        return tarots;
    }

    public async UniTask<List<IPrize>> GetPlanets()
    {
        var planets = new List<IPrize>();
        return planets;
    }
}