using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class EventsService : IService, IPreparable
{
    private ModifierService ModifierService { get; set; }

    public UniTask Prepare()
    {
        var application = Application.Instance;

        ModifierService = application.GetService<ModifierService>();
        return UniTask.CompletedTask;
    }

    public async UniTask DoGameplayStartEvent()
    {
        var tasks = new List<UniTask>();
        
        foreach (IBaseEventData modifier in ModifierService.Modifiers)
        {
            tasks.Add(modifier.DoGameplayStartEvent());
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
        
        await UniTask.WhenAll(tasks);
    }

    public async UniTask DoMinigameEndEvent()
    {
        var tasks = new List<UniTask>();
        
        foreach (IBaseEventData modifier in ModifierService.Modifiers)
        {
            tasks.Add(modifier.DoMinigameEndEvent());
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
        
        await UniTask.WhenAll(tasks);
    }
    
    public async UniTask DoLifeLostEvent()
    {
        var tasks = new List<UniTask>();
        
        foreach (IBaseEventData modifier in ModifierService.Modifiers)
        {
            tasks.Add(modifier.DoLifeLostEvent());
        }
        
        await UniTask.WhenAll(tasks);
    }
    
    public async UniTask DoTimeOverEvent()
    {
        var tasks = new List<UniTask>();
        
        foreach (IBaseEventData modifier in ModifierService.Modifiers)
        {
            tasks.Add(modifier.DoTimeOverEvent());
        }
        
        await UniTask.WhenAll(tasks);
    }
}