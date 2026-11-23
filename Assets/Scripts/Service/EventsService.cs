using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EventsService : IService, IPreparable
{
    private ModifierService ModifierService { get; set; }
    private GameDiscService GameDiscService { get; set; }

    public UniTask Prepare()
    {
        var application = Application.Instance;

        ModifierService = application.GetService<ModifierService>();
        GameDiscService = application.GetService<GameDiscService>();
        return UniTask.CompletedTask;
    }

    public async UniTask DoGameplayStartEvent()
    {
        var tasks = new List<UniTask>();
        
        foreach (IBaseEventData modifier in ModifierService.Modifiers)
        {
            tasks.Add(modifier.DoGameplayStartEvent());
        }

        if (GameDiscService.CurrentGameDisc != null && GameDiscService.CurrentGameDisc is IBaseEventData gameDiscEventData)
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

        if (GameDiscService.CurrentGameDisc != null && GameDiscService.CurrentGameDisc is IBaseEventData gameDiscEventData)
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

        if (GameDiscService.CurrentGameDisc != null && GameDiscService.CurrentGameDisc is IBaseEventData gameDiscEventData)
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

        if (GameDiscService.CurrentGameDisc != null && GameDiscService.CurrentGameDisc is IBaseEventData gameDiscEventData)
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

        if (GameDiscService.CurrentGameDisc != null && GameDiscService.CurrentGameDisc is IBaseEventData gameDiscEventData)
        {
            tasks.Add(gameDiscEventData.DoLifeLostEvent());
        }
        
        await UniTask.WhenAll(tasks);
    }
}