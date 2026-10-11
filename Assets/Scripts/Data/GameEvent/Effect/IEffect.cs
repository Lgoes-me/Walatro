using Cysharp.Threading.Tasks;

public interface IEffect : IComponentData
{
    string[] GetTextParams(Rarity rarity);
    
    UniTask DoEffect();
}

public interface IGameplayStartEffect : IEffect
{

}

public interface IMinigameStartEffect : IEffect
{

}

public interface IMinigameEndEffect : IEffect
{

}

public interface IGameplayEndEffect : IEffect
{

}   

public interface ILifeLostEffect : IEffect
{

}
    
public interface ITimeOverEffect : IEffect
{

}

public interface ICreatedEffect : IEffect
{

}