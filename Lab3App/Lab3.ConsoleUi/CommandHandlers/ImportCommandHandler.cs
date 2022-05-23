using Lab3App.Application.Enums;
using Lab3App.Application.Interfaces;
using Lab3App.Application.Models;

namespace Lab3.ConsoleUi.CommandHandlers;

/// <summary>
/// Handles import command.
/// </summary>
public class ImportCommandHandler : ServiceCommandHandlerBase
{
    private const string JsonType = "json";


    public ImportCommandHandler(IRecordsHandlerService service)
        : base(service)
    {
    }


    protected override string HandlerName { get; set; } = "import";

    public override void Handle(AppCommandRequest commandRequest)
    {
        if (!string.Equals(HandlerName, commandRequest.Command, StringComparison.InvariantCultureIgnoreCase))
        {
            return;
        }
        this.Import(commandRequest.Parameters);
    }
    private void Import(string parameters)
    {
        var args = parameters.Split(" ");
        if (args.Length != 2)
        {
            Console.WriteLine("Wrong command arguments count.");
            return;
        }

        var extension = args[0] switch
        {
            JsonType when args[0].Equals(JsonType, StringComparison.OrdinalIgnoreCase) => FileTypes.Json,
            _ => throw new InvalidOperationException("Can not recognize file type."),
        };

        var path = args[1];
        if (!File.Exists(path))
        {
            Console.WriteLine($"Import error: file {path} is not exist. ");
            return;
        }

        var snapshot = this.Service.MakeSnapshot();
        using (var importFileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            switch (extension)
            {
                case FileTypes.Json:
                    snapshot.LoadFromJson(importFileStream);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(parameters), "Type of file does not supported yet.");
            }
        }

        this.Service.Restore(snapshot);
    }
}