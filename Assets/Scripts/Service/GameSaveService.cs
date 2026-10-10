using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class GameSaveService : IService, IPreparable, IBootable
{
    public Save Save { get; private set; }
    private SaveManager SaveManager { get; set; }

    public UniTask Prepare()
    {
        var application = Application.Instance;
        return UniTask.CompletedTask;
    }

    public UniTask Boot()
    {
        SaveManager = new SaveManager();
        return UniTask.CompletedTask;
    }

    public bool IsFirstTimePlaying()
    {
        foreach (var save in SaveManager.LoadList<Save>())
        {
            if (save.IsValidSave())
                return false;
        }
        
        return true;
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
            if (!save.IsValidSave())
            {
                DeleteSave(save);
                continue;
            }
            
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
        SaveManager.SaveData(Save);
    }

    public void DeleteCurrentGameState()
    {
        DeleteSave(Save);
    }
}