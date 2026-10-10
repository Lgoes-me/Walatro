using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class RandomService : IService, IPreparable, IOnRoomEnter
{
    private GameSaveService GameSaveService { get; set; }
    
    private Random Random { get; set; }
       
    public UniTask Prepare()
    {
        var application = Application.Instance;
        GameSaveService = application.GetService<GameSaveService>();

        return UniTask.CompletedTask;
    }

    public UniTask OnRoomEnter()
    {
        Random = new Random($"{GameSaveService.Save.BaseSeed}".GetHashCode());
        return UniTask.CompletedTask;
    }
    
    public float GetRoomContent(int totalWeight)
    {
        return Random.Next(totalWeight);
    }
    
    public T RandomElement<T>(List<T> list)
    {
        return list[Random.Next(list.Count)];
    }
    
    public int RandomValue(int minimumValue, int maximumValue)
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
        
        //fallback
        return Rarity.Common;
    }
    
    public float RandomFloatValue(float minimumValue, float maximumValue)
    {
        return (float) Random.NextDouble() * (maximumValue - minimumValue) + minimumValue;
    }
}

public enum Rarity
{
    Deactivated = -1,
    Common = 0,
    Uncommon = 1,
    Rare = 2,
    Epic = 3,
    Legendary = 4,
}