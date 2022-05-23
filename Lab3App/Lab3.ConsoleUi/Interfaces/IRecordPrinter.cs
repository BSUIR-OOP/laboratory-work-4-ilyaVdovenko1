using Lab3App.Domain.Interfaces;

namespace Lab3.ConsoleUi.Interfaces;

/// <summary>
/// Implements showing records logic.
/// </summary>
public interface IRecordPrinter
{
    public void Print(IEnumerable<IWorker> records);
}