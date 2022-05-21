using System.Reflection;
using Lab3App.Domain.Entities;
using Lab3App.Domain.Interfaces;

namespace Lab3App.Domain.Models;

public class RecordSnapshot : IRecordSnapshot
{
    private Dictionary<string, PropertyInfo> propertyInfos;

    public dynamic GetRecordData(IWorker workerInfo)
    {

        throw new NotImplementedException();
    }
}