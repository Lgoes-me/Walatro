using System;
using System.Collections.Generic;

[Serializable]
public class Save : ISavable
{
    public Metadata Metadata { get; set; }
    public string BaseSeed { get; set; }
    public List<string> Events { get; set; }
    public List<ModifierInfo> Modifiers { get; set; }
    public int CurrentLevel { get; set; }
    public int CurrentBind { get; set; }
    //public GameBox GameBox { get; set; }

    public Save()
    {
        Metadata = new Metadata().CreateNewDynamicFile(".json");
        BaseSeed = Guid.NewGuid().ToString();
        Events = new List<string>();
        Modifiers = new List<ModifierInfo>();
        CurrentLevel = 1;
        CurrentBind = 0;
        //GameBox = null;
    }
}