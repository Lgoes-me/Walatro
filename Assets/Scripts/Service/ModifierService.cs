using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ModifierService : IService, IPreparable, IBootable, IStartable, IResettable
{
    public List<Modifier> Modifiers { get; private set; }

    private ContentService ContentService { get; set; }
    private InterfaceService InterfaceService { get; set; }
    private GameSaveService GameSaveService { get; set; }
    private PlayerService PlayerService { get; set; }

    public UniTask Prepare()
    {
        var application = Application.Instance;
        
        ContentService = application.GetService<ContentService>();
        InterfaceService = application.GetService<InterfaceService>();
        GameSaveService = application.GetService<GameSaveService>();
        PlayerService = application.GetService<PlayerService>();
        
        return UniTask.CompletedTask;
    }

    public UniTask Boot()
    {
        Modifiers = new List<Modifier>();
        return UniTask.CompletedTask;
    }

    public async UniTask StartSave()
    {
        Modifiers.Clear();

        await LoadModifiers();
    }

    public async UniTask LoadModifiers()
    {        
        foreach (var modifierModel in GameSaveService.Save.Modifiers)
        {
            var modifier = await ContentService.GetModifierFromModel(modifierModel);
            await modifier.DoCreatedEvent();
            Modifiers.Add(modifier);
            
            if (!InterfaceService.DecalManager.ShowDecal(modifier))
            {
                Debug.LogWarning("LIMITE DE MODIFIERS");
            }
        } 
    }

    public UniTask ResetSave()
    {
        ClearModifiers();
        return UniTask.CompletedTask;
    }

    public void AddModifier(Modifier modifier)
    {
        modifier.DoCreatedEvent().Forget();
        Modifiers.Add(modifier);

        if (!InterfaceService.DecalManager.ShowDecal(modifier))
        {
            Debug.LogWarning("LIMITE DE MODIFIERS");
        }
    }

    private void ClearModifiers()
    {
        Modifiers.Clear();
        InterfaceService.DecalManager.HideAll();
    }
}