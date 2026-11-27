using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class RandomService : IService, IPreparable, IStartable, IOnShopEnter
{
    private GameSaveService GameSaveService { get; set; }
    private GameplayService GameplayService { get; set; }
    
    private Random Random { get; set; }
       
    public UniTask Prepare()
    {
        var application = Application.Instance;
        GameSaveService = application.GetService<GameSaveService>();
        GameplayService = application.GetService<GameplayService>();

        return UniTask.CompletedTask;
    }

    [CallbackOrderAttribute(1)]
    public UniTask StartSave()
    {
        Random = new Random($"{GameSaveService.Save.BaseSeed}/{GameplayService.CurrentLevel}/{GameplayService.CurrentBind}".GetHashCode());
        return UniTask.CompletedTask;
    }

    public UniTask OnShopEnter()
    {
        Random = new Random($"{GameSaveService.Save.BaseSeed}/{GameplayService.CurrentLevel}/{GameplayService.CurrentBind}".GetHashCode());
        return UniTask.CompletedTask;
    }
    
    public float GetFloat(int totalWeight)
    {
        return Random.Next(totalWeight);
    }
    
    public T RandomElement<T>(List<T> list)
    {
        if (list.Count == 0)
        {
            return (T) default;
        }

        return list[Random.Next(list.Count)];
    }
    
    public int RandomIntValue(int minimumValue, int maximumValue)
    {
        return Random.Next(minimumValue, maximumValue);
    }
    
    public Rarity RandomRarity()
    {
        var rarities = new Dictionary<Rarity, int>()
        {
            { Rarity.Common, 50 },
            { Rarity.Uncommon, 20 },
            { Rarity.Rare, 15 },
            { Rarity.Epic, 4 },
            { Rarity.Legendary, 1 },
        };
        
        return RandomWightedValue(rarities);
    }
    
    public T RandomWightedValue<T>(Dictionary<T, int> rarities)
    {        
        var pesoTotal = 0;

        foreach (var (_, peso) in rarities)
        {
            pesoTotal += peso;
        }
        
        var valorAleatorio =  Random.Next(pesoTotal);
        
        var acumulo = 0;
        
        foreach (var (rarity, peso) in rarities)
        {
            acumulo += peso;

            if (valorAleatorio >= acumulo) 
                continue;
            
            return rarity;
        }
        
        return default;
    }

    public float RandomFloatValue(float minimumValue, float maximumValue)
    {
        return (float) Random.NextDouble() * (maximumValue - minimumValue) + minimumValue;
    }
}

public enum Rarity
{
    Deactivated = 0,
    Common = 1,
    Uncommon = 2,
    Rare = 3,
    Epic = 4,
    Legendary = 5,
}