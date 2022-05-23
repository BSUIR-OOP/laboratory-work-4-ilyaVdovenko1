
using Lab3.ConsoleUi.CommandHandlers;
using Lab3.ConsoleUi.Interfaces;
using Lab3.ConsoleUi.Models;
using Lab3App.Application.Interfaces;
using Lab3App.Application.Models;
using Lab3App.Domain.Entities;
using Lab3App.Domain.Interfaces;
using Lab3App.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var isRunning = true;

var positions = new Dictionary<string, IWorker>(StringComparer.InvariantCultureIgnoreCase)
{
    {"Developer", new Developer()},
    {"Manager", new Manager()},

};
var snapshot = new RecordSnapshot(positions);

using IHost host = Host.CreateDefaultBuilder(args).ConfigureServices((_, services) =>
    services.AddSingleton<IRecordsHandlerService>(new RecordsHandlerService(snapshot))).Build();

using var serviceScope = host.Services.CreateScope();
var provider = serviceScope.ServiceProvider;

var service = provider.GetRequiredService<IRecordsHandlerService>();


var commandHandler = CreateCommandHandlers(service);



do
{
    Console.Write("> ");
    var line = Console.ReadLine();
    var inputs = line != null ? line.Split(' ', 2) : new[] { string.Empty, string.Empty };
    const int commandIndex = 0;
    var command = inputs[commandIndex];
    

    const int parametersIndex = 1;
    var parameters = inputs.Length > 1 ? inputs[parametersIndex] : string.Empty;

    commandHandler.Handle(new AppCommandRequest(command)
    {
        Parameters = parameters,
    });
}
while (isRunning);


void ChangeAppStatus(bool isAppRunning)
{
    isRunning = isAppRunning;
}

ICommandHandler CreateCommandHandlers(IRecordsHandlerService service)
{
    var createCommandHandler = new CreateCommandHandler(service, snapshot);
    var editCommandHandler = new EditCommandHandler(service, snapshot);
    IRecordPrinter recordPrinter = new DefaultRecordsPrinter(snapshot);
    var importCommandHandler = new ImportCommandHandler(service);
    var listCommandHandler = new ListCommandHandler(service, recordPrinter);
    var removeCommandHandler = new RemoveCommandHandler(service);
    var exportCommandHandler = new ExportCommandHandler(service);
    var exitCommandHandler = new ExitCommandHandler(ChangeAppStatus);
    
    createCommandHandler.SetNext(editCommandHandler);
    editCommandHandler.SetNext(importCommandHandler);
    importCommandHandler.SetNext(listCommandHandler);
    listCommandHandler.SetNext(removeCommandHandler);
    removeCommandHandler.SetNext(exportCommandHandler);
    exportCommandHandler.SetNext(exitCommandHandler);

    return createCommandHandler;
}
