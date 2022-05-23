using Lab3App.Domain.Interfaces;

namespace Lab3App.Application.Interfaces;

public interface IRecordsServiceSnapshot
{
    public IEnumerable<IWorker> Records { get; }
    
    public void SaveToJson(StreamWriter writer);

    public IEnumerable<IWorker> LoadFromJson(FileStream stream);
}