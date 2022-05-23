namespace Lab3App.Domain.Interfaces;

public interface IWorker
{
    public string Position { get; set; }
    
    public Guid WorkerId { get; }
}