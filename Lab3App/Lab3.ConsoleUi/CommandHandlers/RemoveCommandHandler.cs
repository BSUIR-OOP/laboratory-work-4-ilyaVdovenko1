using Lab3App.Application.Interfaces;
using Lab3App.Application.Models;

namespace Lab3.ConsoleUi.CommandHandlers;

public class RemoveCommandHandler : ServiceCommandHandlerBase
{
    public RemoveCommandHandler(IRecordsHandlerService service)
        : base(service)
    {
    }

    protected override string HandlerName { get; set; } = "remove";

    public override void Handle(AppCommandRequest commandRequest)
    {
        if (!string.Equals(HandlerName, commandRequest.Command, StringComparison.InvariantCultureIgnoreCase))
        {
            base.Handle(commandRequest);
        }
        this.Remove(commandRequest.Parameters);
    }
    private void Remove(string parameters)
    {
        if (!int.TryParse(parameters, out var id))
        {
        }

        this.Service.Remove(id);
        Console.WriteLine($"#{id} record has been deleted.");
        
    }
}