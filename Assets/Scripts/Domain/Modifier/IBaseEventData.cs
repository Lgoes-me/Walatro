using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public interface IBaseEventData
{
    BaseEventListener[] Events { get; }
    delegate void EventTriggered();
    
    Rarity Rarity { get; }

    async UniTask DoGameplayStartEvent()
    {
        foreach (var listener in Events)
        {
            if (listener is not GameplayStartListener eventListener)
                continue;

            TriggerEventDelegate();
            await eventListener.Invoke(Rarity);
        }
    }

    async UniTask DoMinigameStartEvent()
    {
        foreach (var listener in Events)
        {
            if (listener is not MinigameStartListener eventListener)
                continue;

            TriggerEventDelegate();
            await eventListener.Invoke(Rarity);
        }
    }

    async UniTask DoMinigameEndEvent()
    {
        foreach (var listener in Events)
        {
            if (listener is not MinigameEndListener eventListener)
                continue;

            TriggerEventDelegate();
            await eventListener.Invoke(Rarity);
        }
    }

    async UniTask DoGameplayEndEvent()
    {
        foreach (var listener in Events)
        {
            if (listener is not GameplayEndListener eventListener)
                continue;

            TriggerEventDelegate();
            await eventListener.Invoke(Rarity);
        }
    }

    async UniTask DoLifeLostEvent()
    {
        foreach (var listener in Events)
        {
            if (listener is not LifeLostListener eventListener)
                continue;

            TriggerEventDelegate();
            await eventListener.Invoke(Rarity);
        }
    }

    async UniTask DoTimeOverEvent()
    {
        foreach (var listener in Events)
        {
            if (listener is not LifeLostListener eventListener)
                continue;

            TriggerEventDelegate();
            await eventListener.Invoke(Rarity);
        }
    }

    string[] GetTextParams(Rarity rarity)
    {
        var texts = new List<string>();
        
        foreach (var listener in Events)
        {
            foreach (var text in listener.BaseEffect.GetTextParams(rarity))
            {
                texts.Add(text);
            }
        }

        return texts.ToArray();
    }
    
    void TriggerEventDelegate();
}
