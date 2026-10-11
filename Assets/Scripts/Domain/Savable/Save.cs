using System;
using System.Collections.Generic;

[Serializable]
public class Save : ISavable
{
    public Metadata Metadata { get; set; }
    public List<string> Events { get; set; }
    public List<ModifierInfo> Modifiers { get; set; }
    public string BaseSeed { get; set; }

    public Save()
    {
        Metadata = new Metadata().CreateNewDynamicFile(".json");
        Events = new List<string>();
        Modifiers = new List<ModifierInfo>();
        BaseSeed = Guid.NewGuid().ToString();
    }
}