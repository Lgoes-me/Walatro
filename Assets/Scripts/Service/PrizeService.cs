using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

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
            PrizeType.Modifier => await GetModifiers(),
            PrizeType.Item => await GetItems(),
            PrizeType.GameDisc => await GetGameDiscs(),
            PrizeType.Tarot => await GetTarots(),
            PrizeType.Planet => await GetPlanets(),
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

    private async UniTask<List<IPrize>> GetModifiers()
    {
        var modifiers = new List<IPrize>();

        for (int i = 0; i < 6; i++)
        {
            var modifierDataAsset = RandomService.RandomElement(ContentService.Modifiers);
            var modifierData = await modifierDataAsset.LoadAssetAsyncIfValid();
            var modifierModel = new ModifierModel(modifierData.Id, RandomService.RandomRarity());
            
            var modifier = modifierData.ToDomain(modifierModel);

            modifiers.Add(modifier);
        }

        return modifiers;
    }

    private async UniTask<List<IPrize>> GetItems()
    {
        var items = new List<IPrize>();
        return items;
    }

    private async UniTask<List<IPrize>> GetTarots()
    {
        var tarots = new List<IPrize>();
        return tarots;
    }

    private async UniTask<List<IPrize>> GetPlanets()
    {
        var planets = new List<IPrize>();
        return planets;
    }

    private async UniTask<List<IPrize>> GetGameDiscs()
    {
        var games = new List<IPrize>();

        for (int i = 0; i < 6; i++)
        {
            var gameDiscDataAsset = RandomService.RandomElement(ContentService.GameDiscs);
            var gameDiscData = await gameDiscDataAsset.LoadAssetAsyncIfValid();
            var gameDiscModel = new GameDiscModel(gameDiscData.Id, DiscModifier.None);
            
            var gameDisc = gameDiscData.ToDomain(gameDiscModel);

            games.Add(gameDisc);
        }

        return games;
    }
}