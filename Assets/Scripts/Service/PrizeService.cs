using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PrizeService : IService, IPreparable
{
    private GameSaveService GameSaveService { get; set; }
    private RandomService RandomService { get; set; }
    private ContentService ContentService { get; set; }
    private ModifierService ModifierService { get; set; }
    private GameDiscService GameDiscService { get; set; }
    private PrefabService PrefabService { get; set; }

    public UniTask Prepare()
    {
        var application = Application.Instance;
        GameSaveService = application.GetService<GameSaveService>();
        RandomService = application.GetService<RandomService>();
        ContentService = application.GetService<ContentService>();
        ModifierService = application.GetService<ModifierService>();
        GameDiscService = application.GetService<GameDiscService>();
        PrefabService = application.GetService<PrefabService>();

        return UniTask.CompletedTask;
    }
    
    public async UniTask<BoosterPack> CreateBoosterPack()
    {
        var sizeRarities = new Dictionary<int, int>()
        {
            { 3, 90 },
            { 5, 10 },
        };
        
        var prizeTypeRarities = new Dictionary<PrizeType, int>()
        {
            { PrizeType.Modifier, 60 },
            { PrizeType.Item, 0 },
            { PrizeType.GameDisc, 40 },
            { PrizeType.Tarot, 0 },
            { PrizeType.Planet, 0 },
        };

        var boosterPack = new BoosterPack(
            RandomService.RandomWightedValue<int>(sizeRarities) + (false ? 1 : 0),
            RandomService.RandomWightedValue<PrizeType>(prizeTypeRarities),
            false
        );

        var prize = await GetPrizeItems(boosterPack.PrizeType);

        boosterPack.SetPrizes(prize);

        return boosterPack;
    }
    
    public async UniTask<List<IPrize>> GetPrizeItems(PrizeType prizeType)
    {
        return prizeType switch
        {
            PrizeType.Modifier => await GetPrizes<ModifierDataAssetReference, ModifierData, Modifier>(ContentService.Modifiers),
            PrizeType.Item => await GetPrizes<ItemDataAssetReference, ItemData, Item>(ContentService.Items),
            PrizeType.GameDisc => await GetPrizes<GameDiscDataAssetReference, GameDiscData, GameDisc>(ContentService.GameDiscs),
            PrizeType.Tarot => await GetPrizes<TarotDataAssetReference, TarotData, Tarot>(ContentService.Tarots),
            PrizeType.Planet => await GetPrizes<PlanetDataAssetReference, PlanetData, Planet>(ContentService.Planets),
            _ => new List<IPrize>(),

        };
    }

    public GameObject InstantiateButton(IPrize prize, Transform parent)
    {
        switch (prize)
        {
            case Modifier modifier:
                var adesivo = PrefabService.InstantiateModifierPrize(modifier, parent);
                adesivo.AddComponent<SpinController>();
                return adesivo;

            case Item item:
                
                break;

            case Tarot tarot:
                
                break;
                
            case Planet planet:
                
                break;
                
            case GameDisc gameDisc:
                var disc = PrefabService.InstantiateGameDisc(gameDisc, parent, parent).gameObject;
                disc.transform.rotation = Quaternion.Euler(80, -178, 9);
                disc.transform.localScale *= 0.75f;

                disc.AddComponent<SpinController>();

                return disc;

            default:
                break;
        }

        return null;
    }

    public void AddPrize(IPrize prize)
    {
        switch (prize)
        {
            case Modifier modifier:
                ModifierService.AddModifier(modifier);
                break;

            case Item item:
                
                break;

            case Tarot tarot:
                
                break;
                
            case Planet planet:
                
                break;
                
            case GameDisc gameDisc:
                GameDiscService.ReceiveNewDisc(gameDisc);
                break;

            default:
                break;
        }

    }

    private async UniTask<List<IPrize>> GetPrizes<T1, T2, T3>(List<T1> contentList) 
        where T1 : AssetReferenceT<T2>
        where T2 : PrizeData<T3>
        where T3 : IPrize
    {
        var prizes = new List<IPrize>();
        var prizeList = new List<T1>(contentList);

        for (int i = 0; i < 6; i++)
        {
            var prizeDataAsset = RandomService.RandomElement(prizeList);
            
            if (prizeDataAsset == null)
                break;
            
            prizeList.Remove(prizeDataAsset);

            var prizeData = await prizeDataAsset.LoadAssetAsyncIfValid();
            var prize = prizeData.ToDomain();

            prizes.Add(prize);
        }

        return prizes;
    }
}