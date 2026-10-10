using System;
using System.Collections.Generic;

[Serializable]
public class Save : ISavable
{
    public Metadata Metadata { get; set; }
    public List<string> Events { get; set; }
    public string BaseSeed { get; set; }

    public Save()
    {
        Metadata = new Metadata().CreateNewDynamicFile(".json");
        Events = new List<string>();
        BaseSeed = Guid.NewGuid().ToString();
    }

    public bool IsValidSave()
    {
        return Events != null && Events.Contains("FirstDeath");
    }
}