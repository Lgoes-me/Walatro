using System;
using System.Collections.Generic;

[Serializable]
public class Save : ISavable
{
    public Metadata Metadata { get; set; }
    public string BaseSeed { get; set; }
    public List<string> Events { get; set; }
    public List<ModifierModel> Modifiers { get; set; }
    public int CurrentLevel { get; set; }
    public int CurrentBind { get; set; }
    public GameBoxModel GameBox { get; set; }

    public Save()
    {
        Metadata = new Metadata().CreateNewDynamicFile(".json");
        BaseSeed = Guid.NewGuid().ToString();
        Events = new List<string>();
        Modifiers = new List<ModifierModel>() {new ModifierModel("ccc25c7feb9cb1d4cae56ccce3463063", Rarity.Common)};
        CurrentLevel = 1;
        CurrentBind = 0;
        GameBox = new GameBoxModel(new List<GameDiscModel>());
    }
}