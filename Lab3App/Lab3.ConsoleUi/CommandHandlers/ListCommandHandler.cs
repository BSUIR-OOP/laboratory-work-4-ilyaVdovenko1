using Lab3.ConsoleUi.Interfaces;
using Lab3App.Application.Interfaces;
using Lab3App.Application.Models;
using Lab3App.Domain.Interfaces;

namespace Lab3.ConsoleUi.CommandHandlers;

public class ListCommandHandler : ServiceCommandHandlerBase
{
    private readonly IRecordPrinter printer;


    public ListCommandHandler(IRecordsHandlerService service, IRecordPrinter printer)
        : base(service)
    {
        this.printer = printer;
    }
    
    protected override string HandlerName { get; set; } = "list";

    public override void Handle(AppCommandRequest commandRequest)
    {
        if (!string.Equals(HandlerName, commandRequest.Command, StringComparison.InvariantCultureIgnoreCase))
        {
            base.Handle(commandRequest);
        }
        this.List();
    }
    private void List()
    {
        var arrayOfRecords =new List<IWorker>(this.Service.GetWorkers()); 
        if (arrayOfRecords.Count == 0)
        {
            Console.WriteLine("There are no records.");
        }

        this.printer.Print(arrayOfRecords);
    }
}