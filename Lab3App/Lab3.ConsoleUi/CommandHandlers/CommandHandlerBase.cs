using Lab3App.Application.Interfaces;
using Lab3App.Application.Models;

namespace Lab3.ConsoleUi.CommandHandlers;

public abstract class CommandHandlerBase : ICommandHandler
{
    protected virtual string HandlerName { get; set; }
    private ICommandHandler? nextHandler;
    
    public void SetNext(ICommandHandler handler)
    {
        this.nextHandler = handler;
    }
    
    public virtual void Handle(AppCommandRequest commandRequest)
    {
        this.nextHandler?.Handle(commandRequest);
    }
}