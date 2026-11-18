using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerService : IService, IPreparable, IStartable, IOnGameOver
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
    
    public UniTask StartSave()
    {
        //todo nao deveria vir do save
        /*

        foreach (var modifierModel in GameSaveService.Save.Modifiers)
        {
            var modifier = await ContentService.GetModifierFromModel(modifierModel);
            PlayerModifiers.Add(modifier);
        }*/
        
        return UniTask.CompletedTask;
    }
    
    public UniTask OnGameOver()
    {
        //PlayerModifiers.Clear();
        return UniTask.CompletedTask;
    }
}
