using Lab3App.Application.Interfaces;

namespace Lab3.ConsoleUi.CommandHandlers;

public abstract class ServiceCommandHandlerBase : CommandHandlerBase
{
    protected ServiceCommandHandlerBase(IRecordsHandlerService service)
    {
        this.Service = service;
    }
    
    protected IRecordsHandlerService Service { get; }
}