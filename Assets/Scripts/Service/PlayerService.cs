using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerService : IService, IPreparable, IStartable, IResettable
{
    public List<Modifier> PlayerModifiers { get; private set; }

    private GameSaveService GameSaveService { get; set; }
    private ContentService ContentService { get; set; }
    
    public UniTask Prepare()
    {
        var application = Application.Instance;
        GameSaveService = application.GetService<GameSaveService>();
        ContentService = application.GetService<ContentService>();

        return UniTask.CompletedTask;
    }
    
    [CallbackOrder(0)]
    public async UniTask StartSave()
    {
        PlayerModifiers = new List<Modifier>();

        foreach (var modifierInfo in GameSaveService.Save.Modifiers)
        {
            var modifier = await ContentService.GetModifierFromInfo(modifierInfo);
            PlayerModifiers.Add(modifier);
        }
    }
    
    public UniTask ResetSave()
    {
        PlayerModifiers.Clear();
        return UniTask.CompletedTask;
    }
}
