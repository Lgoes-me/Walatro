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

    public async UniTask Invoke()
    {
        if (Validator != null && !Validator.Validate())
            return;

        await Effect.DoEffect();
    }
}

public class GameplayStartListener : BaseEventListener<IGameplayStartValidator, IGameplayStartEffect>
{

}

public class MinigameStartListener : BaseEventListener<IMinigameStartValidator, IMinigameStartEffect>
{

}

public class MinigameEndListener : BaseEventListener<IMinigameEndValidator, IMinigameEndEffect>
{

}


public class GameplayEndListener : BaseEventListener<IGameplayEndValidator, IGameplayEndEffect>
{

}

public class LifeLostListener : BaseEventListener<ILifeLostValidator, ILifeLostEffect>
{

}

public class TimeOverListener : BaseEventListener<ITimeOverValidator, ITimeOverEffect>
{

}

public class CreatedListener : BaseEventListener<ICreatedValidator, ICreatedEffect>
{

}