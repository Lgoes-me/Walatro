using System;
using System.Text;

public class Metadata
{
    public string FileName { get; set; }
    public int Version { get; set; }
    public DateTime LastSaved { get; set; }
    public TimeSpan Age => DateTime.UtcNow - LastSaved;

    public Metadata()
    {
        FileName = String.Empty;
        Version = -1;
        LastSaved = DateTime.MinValue;
    }

    public Metadata CreateNewStaticFile(string fileName, string extension)
    {
        var stringBuilder = new StringBuilder()
            .Append(fileName)
            .Append(extension);
        
        FileName = stringBuilder.ToString();
        Version = 1;
        LastSaved = DateTime.UtcNow;

        return this;
    }
    
    public Metadata CreateNewDynamicFile(string extension)
    {
        var stringBuilder = new StringBuilder()
            .Append(Guid.NewGuid().ToString())
            .Append(extension);
        
        FileName = stringBuilder.ToString();
        Version = 1;
        LastSaved = DateTime.UtcNow;

        return this;
    }
}