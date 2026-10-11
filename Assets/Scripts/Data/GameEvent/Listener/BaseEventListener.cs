using System;
using UnityEngine;

[Serializable]
public abstract class BaseEventListener : IComponentData
{
    public virtual IEffect BaseEffect { get; protected set; }
}

[Serializable]
public abstract class BaseEventListener<T, T1>: BaseEventListener 
    where T : IEventValidator 
    where T1 : IEffect
{
    [field: SerializeReference] protected T Validator { get; set; }
    [field: SerializeReference] protected T1 Effect { get; set; }

    public override IEffect BaseEffect => Effect;
}