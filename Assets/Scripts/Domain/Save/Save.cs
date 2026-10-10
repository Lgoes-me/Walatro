using System;
using System.Collections.Generic;

[Serializable]
public class Save : ISavable
{
    public Metadata Metadata { get; set; }

    public Save()
    {
        Metadata = new Metadata().CreateNewDynamicFile(".json");
    }

    public bool IsValidSave()
    {
        return true;//return Events != null && Events.Contains("FirstDeath");
    }
}