using Lab3App.Application.Models;

namespace Lab3App.Application.Interfaces;

public interface ICommandHandler
{
    public void SetNext(ICommandHandler handler);
    
    public void Handle(AppCommandRequest commandRequest);
}