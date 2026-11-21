using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EventsService : IService, IPreparable
{
    private ModifierService ModifierService { get; set; }
    private GameBoxService GameBoxService { get; set; }

    public UniTask Prepare()
    {
        var application = Application.Instance;

        ModifierService = application.GetService<ModifierService>();
        GameBoxService = application.GetService<GameBoxService>();
        return UniTask.CompletedTask;
    }

    public async UniTask DoGameplayStartEvent()
    {
        var tasks = new List<UniTask>();
        
        foreach (IBaseEventData modifier in ModifierService.Modifiers)
        {
            tasks.Add(modifier.DoGameplayStartEvent());
        }

        if (GameBoxService.CurrentGameDisc != null && GameBoxService.CurrentGameDisc is IBaseEventData gameDiscEventData)
        {
            tasks.Add(gameDiscEventData.DoGameplayStartEvent());
        }

        await UniTask.WhenAll(tasks);
    }

    public async UniTask DoMinigameStartEvent()
    {
        var tasks = new List<UniTask>();
        
        foreach (IBaseEventData modifier in ModifierService.Modifiers)
        {
            tasks.Add(modifier.DoMinigameStartEvent());
        }

        if (GameBoxService.CurrentGameDisc != null && GameBoxService.CurrentGameDisc is IBaseEventData gameDiscEventData)
        {
            tasks.Add(gameDiscEventData.DoMinigameStartEvent());
        }
        
        await UniTask.WhenAll(tasks);
    }

    public async UniTask DoMinigameEndEvent()
    {
        var tasks = new List<UniTask>();
        
        foreach (IBaseEventData modifier in ModifierService.Modifiers)
        {
            tasks.Add(modifier.DoMinigameEndEvent());
        }

        if (GameBoxService.CurrentGameDisc != null && GameBoxService.CurrentGameDisc is IBaseEventData gameDiscEventData)
        {
            tasks.Add(gameDiscEventData.DoMinigameEndEvent());
        }
        
        await UniTask.WhenAll(tasks);
    }

    public async UniTask DoGameplayEndEvent()
    {
        var tasks = new List<UniTask>();
        
        foreach (IBaseEventData modifier in ModifierService.Modifiers)
        {
            tasks.Add(modifier.DoGameplayEndEvent());
        }

        if (GameBoxService.CurrentGameDisc != null && GameBoxService.CurrentGameDisc is IBaseEventData gameDiscEventData)
        {
            tasks.Add(gameDiscEventData.DoGameplayEndEvent());
        }
        
        await UniTask.WhenAll(tasks);
    }
    
    public async UniTask DoLifeLostEvent()
    {
        var tasks = new List<UniTask>();
        
        foreach (IBaseEventData modifier in ModifierService.Modifiers)
        {
            tasks.Add(modifier.DoLifeLostEvent());
        }

        if (GameBoxService.CurrentGameDisc != null && GameBoxService.CurrentGameDisc is IBaseEventData gameDiscEventData)
        {
            tasks.Add(gameDiscEventData.DoLifeLostEvent());
        }
        
        await UniTask.WhenAll(tasks);
    }
}