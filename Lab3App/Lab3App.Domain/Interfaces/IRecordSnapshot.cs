using System.Reflection;
using Lab3App.Domain.Entities;

namespace Lab3App.Domain.Interfaces;

public interface IRecordSnapshot
{
    public Dictionary<string, IWorker> Positions { get; }
    public Dictionary<string,string> RepresentRecordAsString(IWorker workerInfo);

    public IWorker? FillWorkerWithData(Dictionary<string, object> workerData, IWorker worker);

    public Dictionary<string, object> GetRecordData(IWorker workerInfo);

    public IEnumerable<string> GetPropertyNames(IWorker worker);

    public IEnumerable<PropertyInfo> GetProperties(IWorker worker);

}