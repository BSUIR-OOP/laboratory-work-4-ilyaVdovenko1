using Lab3App.Domain.Entities;

namespace Lab3App.Domain.Interfaces;

public interface IRecordSnapshot
{
    public dynamic GetRecordData(IWorker workerInfo);
    
}