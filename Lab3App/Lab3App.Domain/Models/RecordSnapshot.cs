using System.Reflection;
using Lab3App.Domain.Entities;
using Lab3App.Domain.Interfaces;
using Lab3App.Extensions;

namespace Lab3App.Domain.Models;

public class RecordSnapshot : IRecordSnapshot
{
    private readonly Dictionary<string, IWorker> positions;

    public RecordSnapshot(Dictionary<string, IWorker> positions)
    {
        this.positions = positions;
        Positions = positions;
    }

    public Dictionary<string, IWorker> Positions { get; }

    public Dictionary<string, string> RepresentRecordAsString(IWorker workerInfo)
    {
        var typeOfWorker = workerInfo.GetType().TypesImplementingInterface().Last(type => string.Equals(type.Name, workerInfo.Position, StringComparison.InvariantCultureIgnoreCase));
        var properties = typeOfWorker.GetClassProperties();
        var worker = Convert.ChangeType(workerInfo, typeOfWorker);

        return properties.ToDictionary(property => property.Name, property => property.GetValue(worker).ToString() ?? throw new ArgumentException("Reading data problems"));

    }

    public IWorker? FillWorkerWithData(Dictionary<string, object> workerData, IWorker worker)
    {
        var typeOfWorker = worker.GetType().TypesImplementingInterface().Last(type => string.Equals(type.Name, worker.Position, StringComparison.InvariantCultureIgnoreCase));
        var properties = typeOfWorker.GetClassProperties();
        var filledWorker = Convert.ChangeType(worker, typeOfWorker);

        filledWorker = filledWorker.GetType().FillStringPropertiesRecursively(filledWorker, workerData);

        return filledWorker as IWorker;
    }

    public Dictionary<string, object> GetRecordData(IWorker workerInfo)
    {
        var typeOfWorker = workerInfo.GetType().TypesImplementingInterface().Last(type => string.Equals(type.Name, workerInfo.Position, StringComparison.InvariantCultureIgnoreCase));
        var properties = typeOfWorker.GetClassProperties();
        var worker = Convert.ChangeType(workerInfo, typeOfWorker);

        return properties.ToDictionary(property => property.Name, property => property.GetValue(worker) ?? throw new ArgumentException("Reading data problems"));
    }

    public IEnumerable<string> GetPropertyNames(IWorker worker)
    {
        return worker.GetType().TypesImplementingInterface().Last(type => string.Equals(type.Name, worker.Position, StringComparison.InvariantCultureIgnoreCase)).GetClassProperties().Select(p=>p.Name );
        
    }

    public IEnumerable<PropertyInfo> GetProperties(IWorker worker)
    {
        return worker.GetType().TypesImplementingInterface().Last(type =>
                string.Equals(type.Name, worker.Position, StringComparison.InvariantCultureIgnoreCase))
            .GetClassProperties();
    }
}