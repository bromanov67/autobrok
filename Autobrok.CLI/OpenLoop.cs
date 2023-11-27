using System.IO.Enumeration;
using System.Text.Json;

public class OpenLoop
{
    public string Note { get; set; }
    public DateTimeOffset CreateDate { get; set;}
}

public class OpenLoopsRepository
{
    private const string DirecoryName = "./openLoops/";
    public bool Add(OpenLoop newOpenLoop)
    {
        Directory.CreateDirectory(DirecoryName);

        var json = JsonSerializer.Serialize(newOpenLoop, new JsonSerializerOptions { WriteIndented = true });

        var filename = $"{Guid.NewGuid()}.json";
        var filePath = Path.Combine(DirecoryName, filename);

        File.WriteAllText(filePath, json);

        return true;
    }
    public OpenLoop[] Get()
    {
        var files = Directory.GetFiles(DirecoryName);
        var openLoops = new List<OpenLoop>();

        foreach (var file in files)
        {
            var json = File.ReadAllText(file);
            var openLoop = JsonSerializer.Deserialize<OpenLoop>(json);
            
            if (openLoop == null)
            {
                throw new Exception("OpenLoop can't be serialized");
            }

            openLoops.Add(openLoop);
        }
        return openLoops.ToArray();
        
    }
}