using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

[Serializable]
public abstract class BaseEventListener : IComponentData
{
    public virtual IEffect BaseEffect { get; protected set; }
}

[Serializable]
public abstract class BaseEventListener<T, T1>: BaseEventListener 
    where T : IValidator 
    where T1 : IEffect
{
    [field: SerializeReference] protected T Validator { get; set; }
    [field: SerializeReference] protected T1 Effect { get; set; }

    public override IEffect BaseEffect => Effect;

    protected BaseEventListener(T validator, T1 effect)
    {
        Validator = validator;
        Effect = effect;
    }

    public async UniTask Invoke(Rarity rarity)
    {
        if (Validator != null && !Validator.Validate())
            return;

        await Effect.DoEffect(rarity);
    }
}

[Serializable]
public class GameplayStartListener : BaseEventListener<IGameplayStartValidator, IGameplayStartEffect>
{
    public GameplayStartListener(IGameplayStartValidator validator, IGameplayStartEffect effect) : base(validator, effect)
    {
        
    }
}

[Serializable]
public class MinigameStartListener : BaseEventListener<IMinigameStartValidator, IMinigameStartEffect>
{
    public MinigameStartListener(IMinigameStartValidator validator, IMinigameStartEffect effect) : base(validator, effect)
    {

    }
}

[Serializable]
public class MinigameEndListener : BaseEventListener<IMinigameEndValidator, IMinigameEndEffect>
{
    public MinigameEndListener(IMinigameEndValidator validator, IMinigameEndEffect effect) : base(validator, effect)
    {
        
    }
}

[Serializable]
public class GameplayEndListener : BaseEventListener<IGameplayEndValidator, IGameplayEndEffect>
{
    public GameplayEndListener(IGameplayEndValidator validator, IGameplayEndEffect effect) : base(validator, effect)
    {
        
    }
}

[Serializable]
public class LifeLostListener : BaseEventListener<ILifeLostValidator, ILifeLostEffect>
{
    public LifeLostListener(ILifeLostValidator validator, ILifeLostEffect effect) : base(validator, effect)
    {
        
    }
}

[Serializable]
public class CreatedListener : BaseEventListener<ICreatedValidator, ICreatedEffect>
{
    public CreatedListener(ICreatedValidator validator, ICreatedEffect effect) : base(validator, effect)
    {
        
    }
}