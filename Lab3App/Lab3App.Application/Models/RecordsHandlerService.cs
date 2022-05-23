using System.Collections.ObjectModel;
using Lab3App.Application.Interfaces;
using Lab3App.Domain.Entities;
using Lab3App.Domain.Interfaces;
using Lab3App.Domain.Models;

namespace Lab3App.Application.Models;

public class RecordsHandlerService : IRecordsHandlerService
{
    private readonly Dictionary<string, IWorker> positions = new()
    {
        {"Developer", new Developer()},
        {"Manager", new Manager()}
    };

    private readonly IRecordSnapshot recordsData;

    private List<IWorker> workers;
    public RecordsHandlerService()
    {
        this.recordsData = new RecordSnapshot(positions);
        this.workers = new List<IWorker>();
    }

    public RecordsHandlerService(IRecordSnapshot snapshot)
    {
        this.recordsData = snapshot;
        positions = snapshot.Positions;
        this.workers = new List<IWorker>();
    }
    
    public Guid CreateRecord(UserRecordData data)
    {
        if (!positions.ContainsKey(data.Position))
        {
            throw new ArgumentException($"There are no such position: {data.Position}");
        }

        var worker = positions[data.Position];
        worker = recordsData.FillWorkerWithData(data.Data!, worker);

        if (worker is null)
        {
            throw new ArgumentException("Record was not created!");
        }
        workers.Add(worker);
        
        return worker.WorkerId;

    }

    public void EditRecord(Guid id, UserRecordData newData)
    {
        if (!positions.ContainsKey(newData.Position))
        {
            throw new ArgumentException($"There are no such position: {newData.Position}");
        }

        var oldRecord = Remove(id);
        var worker = recordsData.FillWorkerWithData(newData.Data!, oldRecord);
        if (worker is null)
        {
            throw new ArgumentException("Record was not created!");
        }
        workers.Add(worker);


    }
    
    public void EditRecord(int orderInCurrent, UserRecordData newData)
    {

        if (!positions.ContainsKey(newData.Position))
        {
            throw new ArgumentException($"There are no such position: {newData.Position}");
        }

        var oldRecord = Remove(orderInCurrent);
        var worker = recordsData.FillWorkerWithData(newData.Data!, oldRecord);
        
        if (worker is null)
        {
            throw new ArgumentException("Record was not created!");
        }
        workers.Add(worker);
    }
    

    public IEnumerable<IWorker> GetWorkers()
    {
        return workers;
    }

    public int GetStat()
    {
        return workers.Count;
    }

    public IRecordsServiceSnapshot MakeSnapshot()
    {
        if (this.workers.Count == 0)
        {
            throw new ArgumentException("There are no records.");
        }

        return new RecordsServiceSnapshot(workers, recordsData);
    }

    public void Restore(IRecordsServiceSnapshot snapshot)
    {
        workers = new List<IWorker>();
        workers.AddRange(snapshot.Records);
    }

    public IWorker Remove(int id)
    {
        if (id < 0 || id > this.workers.Count)
        {
            throw new ArgumentException($"#{id} record is not found.", nameof(id));
        }

        
        return Remove(workers[id].WorkerId);
    }

    public IWorker Remove(Guid workerId)
    {
        var orderedId = 0;
        foreach (var worker in workers)
        {
            orderedId += 1;
            if (workerId != worker.WorkerId) continue;
            workers.RemoveAt(orderedId);
            return worker;
        }

        throw new ArgumentException($"No records with such id: {workerId}");
    }
}