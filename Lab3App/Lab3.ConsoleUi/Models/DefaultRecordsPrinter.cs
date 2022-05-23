using Lab3.ConsoleUi.Interfaces;
using Lab3App.Domain.Interfaces;

namespace Lab3.ConsoleUi.Models;

public class DefaultRecordsPrinter : IRecordPrinter
{
    private readonly IRecordSnapshot snapshot;
    public DefaultRecordsPrinter(IRecordSnapshot snapshot)
    {
        this.snapshot = snapshot;
    }

    public void Print(IEnumerable<IWorker> records)
    {
        foreach (var record in records)
        {
            var stringInfo = snapshot.RepresentRecordAsString(record);

            foreach (var (key, value) in stringInfo)
            {
                Console.WriteLine($"{key}:{value}");
            }
        }
    }
}