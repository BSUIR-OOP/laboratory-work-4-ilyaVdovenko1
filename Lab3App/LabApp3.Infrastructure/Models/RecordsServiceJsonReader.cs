using System.Text.Json.Nodes;

namespace LabApp3.Infrastructure.Models;

public class RecordsServiceJsonReader
{
    private readonly TextReader reader;
    public RecordsServiceJsonReader(TextReader reader)
    {
        this.reader = reader;
    }

    public JsonNode? ReadAll()
    {
        return JsonNode.Parse(reader.ReadToEnd());
        
    }
}