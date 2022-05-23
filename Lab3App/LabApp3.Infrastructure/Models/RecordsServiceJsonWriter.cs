
using System.Text.Json.Nodes;
using LabApp3.Infrastructure.Interfaces;

namespace LabApp3.Infrastructure.Models;

public class RecordsServiceJsonWriter : IRecordsServiceJsonWriter
{
    private readonly TextWriter writer;
    public RecordsServiceJsonWriter(TextWriter writer)
    {
        this.writer = writer;
    }
    public void Write(Dictionary<string,string> data)
    {
        foreach (var dataElement in data)
        {
            this.writer.WriteLine($"{dataElement.Key}:{dataElement.Value}");
        }
    }

    public void Write(JsonObject jsonObject)
    {
        var text = jsonObject.ToString();
        writer.Write(text);
    }
}