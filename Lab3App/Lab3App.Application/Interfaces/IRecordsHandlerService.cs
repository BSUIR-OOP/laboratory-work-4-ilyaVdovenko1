using System.Collections.ObjectModel;
using Lab3App.Application.Models;
using Lab3App.Domain.Interfaces;

namespace Lab3App.Application.Interfaces;

public interface IRecordsHandlerService
{

    public Guid CreateRecord(UserRecordData data);
    
    public void EditRecord(Guid id, UserRecordData data);

    public void EditRecord(int orderInCurrent, UserRecordData newData);

    public int GetStat();
    
    public IRecordsServiceSnapshot MakeSnapshot();
    
    public void Restore(IRecordsServiceSnapshot snapshot);

    public IWorker Remove(int id);
    
    public IWorker Remove(Guid id);

    public IEnumerable<IWorker> GetWorkers();
}