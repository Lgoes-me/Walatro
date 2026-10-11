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

[Serializable]
public class GameplayStartListener : BaseEventListener<IGameplayStartValidator, IGameplayStartEffect>
{

}

[Serializable]
public class MinigameStartListener : BaseEventListener<IMinigameStartValidator, IMinigameStartEffect>
{

}

[Serializable]
public class MinigameEndListener : BaseEventListener<IMinigameEndValidator, IMinigameEndEffect>
{

}

[Serializable]
public class GameplayEndListener : BaseEventListener<IGameplayEndValidator, IGameplayEndEffect>
{

}

[Serializable]
public class LifeLostListener : BaseEventListener<ILifeLostValidator, ILifeLostEffect>
{

}

[Serializable]
public class TimeOverListener : BaseEventListener<ITimeOverValidator, ITimeOverEffect>
{

}

[Serializable]
public class CreatedListener : BaseEventListener<ICreatedValidator, ICreatedEffect>
{

}