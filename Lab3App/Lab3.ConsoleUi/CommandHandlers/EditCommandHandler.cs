using Lab3App.Application.Interfaces;
using Lab3App.Application.Models;
using Lab3App.Domain.Interfaces;

namespace Lab3.ConsoleUi.CommandHandlers;

/// <summary>
/// Handles edit command.
/// </summary>
public class EditCommandHandler : CommandHandlersWithSnapshot
{
    
    
    public EditCommandHandler(IRecordsHandlerService service, IRecordSnapshot snapshot)
        : base(service, snapshot)
    {
    }

    protected override string HandlerName { get; set; } = "edit";

    public override void Handle(AppCommandRequest commandRequest)
    {
        if (!string.Equals(HandlerName, commandRequest.Command, StringComparison.InvariantCultureIgnoreCase))
        {
            base.Handle(commandRequest);
            return;
        }
        this.Edit(commandRequest.Parameters);
    }
    private void Edit(string parameters)
    {
        Console.Write("Position: ");
        var positionString = ReadInput(StringConverter);

        if (!Snapshot.Positions.ContainsKey(positionString))
        {
            throw new ArgumentException("There are no such position");
        }

        var worker = Snapshot.Positions[positionString];

        var data = ReadInput(worker);
        
        if (!int.TryParse(parameters, out var recordId) || recordId < 0 || recordId > this.Service.GetStat())
        {
            if (Guid.TryParse(parameters, out var guid))
            {
                Service.EditRecord(guid, data);
            }

            throw new ArgumentException("There are no such record");
        }
        
        Service.EditRecord(recordId, data);
            
        
    }
    
}