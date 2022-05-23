using Lab3App.Application.Interfaces;
using Lab3App.Application.Models;
using Lab3App.Domain.Interfaces;

namespace Lab3.ConsoleUi.CommandHandlers;

public class CreateCommandHandler : CommandHandlersWithSnapshot
{

    public CreateCommandHandler(IRecordsHandlerService service, IRecordSnapshot snapshot)
        : base(service, snapshot)
    {
    }

    protected override string HandlerName { get; set; } = "create";

    public override void Handle(AppCommandRequest commandRequest)
    {
        if (!string.Equals(HandlerName, commandRequest.Command, StringComparison.InvariantCultureIgnoreCase))
        {
            base.Handle(commandRequest);
        }
        this.Create();
    }

    private void Create()
    {
        Console.Write("Position: ");
        var positionString = ReadInput(StringConverter);

        if (!Snapshot.Positions.ContainsKey(positionString))
        {
            throw new ArgumentException("There are no such position");
        }

        var worker = Snapshot.Positions[positionString];

        var data = ReadInput(worker);

        Service.CreateRecord(data);


    }

}