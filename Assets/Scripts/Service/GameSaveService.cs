using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class GameSaveService : IService, IPreparable, IBootable
{
    public Save Save { get; private set; }
    private SaveManager SaveManager { get; set; }

    private ModifierService ModifierService { get; set; }
    private GameplayService GameplayService { get; set; }

    public UniTask Prepare()
    {
        var application = Application.Instance;
        ModifierService = application.GetService<ModifierService>();
        GameplayService = application.GetService<GameplayService>();
        return UniTask.CompletedTask;
    }

    public UniTask Boot()
    {
        SaveManager = new SaveManager();
        return UniTask.CompletedTask;
    }

    public bool PossuiContinue()
    {
        foreach (var save in SaveManager.LoadList<Save>())
        {
            return true;
        }
        
        return false;
    }

    public void StartNewSave()
    {
        Save = new Save();
    }
    
    //File
    public void LoadLastSave()
    {
        Save = LoadSaveList()[0];
    }
    
    public List<Save> LoadSaveList()
    {
        var saves = new List<Save>();

        foreach (var save in SaveManager.LoadList<Save>())
        {                       
            saves.Add(save);
        }

        saves.Sort((x, y) => x.Metadata.Age.CompareTo(y.Metadata.Age));

        return saves;
    }

    public void DeleteSave(Save savedGame)
    {
        SaveManager.DeleteData<Save>(savedGame);
    }
    
    public void SaveCurrentGameState()
    {
        Save.Modifiers.Clear();
        
        foreach (var modifier in ModifierService.Modifiers)
        {
            Save.Modifiers.Add(new ModifierInfo()
            {
                Id = modifier.Id,
                Rarity = modifier.Rarity,
            });
        }

        Save.CurrentLevel = GameplayService.CurrentLevel;
        Save.CurrentBind = GameplayService.CurrentBind;

        SaveManager.SaveData(Save);
    }

    public void DeleteCurrentGameState()
    {
        DeleteSave(Save);
    }
}