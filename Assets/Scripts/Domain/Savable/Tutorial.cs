using System.Collections.Generic;

public class Tutorial : ISavable
{
    public Metadata Metadata { get; set; }
    public List<string> Shown { get; set; }

    public Tutorial()
    {
        Metadata = new Metadata().CreateNewStaticFile("Tutorial", ".json");
        Shown = new List<string>();
    }
}