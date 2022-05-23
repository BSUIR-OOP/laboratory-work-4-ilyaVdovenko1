using System.Text.Json.Nodes;
using Lab3App.Application.Interfaces;
using Lab3App.Domain.Interfaces;
using LabApp3.Infrastructure.Models;

namespace Lab3App.Application.Models;

public class RecordsServiceSnapshot : IRecordsServiceSnapshot
{
    public IEnumerable<IWorker> Records { get; }

    private IRecordSnapshot snapshot;
    public RecordsServiceSnapshot(IEnumerable<IWorker> records, IRecordSnapshot snapshot)
    {
        this.snapshot = snapshot;
        this.Records = records;
    }

    public void SaveToJson(StreamWriter writer)
    {
        using var textWriter = (TextWriter)writer;

        var jsonObject = new JsonObject();
        var workers = new JsonArray();
        foreach (var worker in Records)
        {
            var workerData = snapshot.GetRecordData(worker);
            workers.Add(workerData);
        }
        jsonObject.Add("workers", workers);
        var jsonWriter = new RecordsServiceJsonWriter(textWriter);
        jsonWriter.Write(jsonObject);
    }

    public IEnumerable<IWorker> LoadFromJson(FileStream stream)
    {
        using var readerStream = new StreamReader(stream);
        using var readerTextStream = (TextReader)readerStream;
        var reader = new RecordsServiceJsonReader(readerTextStream);
        var parsedRecords = reader.ReadAll();
        var workersData = parsedRecords["workers"].AsArray();
        var workers = new List<IWorker>();
        foreach (var workerData in workersData)
        {
            var workerObject = workerData.AsObject();
            var position = workerObject["Position"]!.AsValue().ToString();
            var worker = this.snapshot.Positions[position];
            var properties = snapshot.GetPropertyNames(worker);
            var propertiesWithValues = properties.ToDictionary<string?, string, object>(property => property, property => workerObject[property]!);

            workers.Add(snapshot.FillWorkerWithData(propertiesWithValues, worker));
            
            
        }

        return workers;
    }
}