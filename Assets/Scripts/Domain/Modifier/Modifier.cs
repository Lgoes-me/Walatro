using UnityEngine;
using Cysharp.Threading.Tasks;

public class Modifier : IBaseEventData, IInfoDescriptor
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public Material Material { get; private set; }
    public Rarity Rarity { get; private set; }

    public BaseEventListener[] Events { get; }
    private string Description { get; set; }
    
    public event IBaseEventData.EventTriggered ModifierTriggered;

    public Modifier(
        string id, 
        string name,
        Material material, 
        Rarity rarity, 
        BaseEventListener[] events, 
        string description)
    {
        Id = id;
        Name = name;
        Material = material;
        Rarity = rarity;
        Events = events;
        Description = description;
    }

    public async UniTask DoCreatedEvent()
    {
        foreach (var listener in Events)
        {
            if (listener is not CreatedListener eventListener)
                continue;

            TriggerEventDelegate();
            await eventListener.Invoke(Rarity);
        }
    }
    
    public void TriggerEventDelegate()
    {
        ModifierTriggered?.Invoke();
    }

    public string GetDescription(Rarity rarity) => string.Format(Description ?? string.Empty, (this as IBaseEventData).GetTextParams(rarity));
}