namespace Lab3App.Application.Models;

public class AppCommandRequest
{
    private readonly string? parameters;
    
    public AppCommandRequest(string command)
    {
        this.Command = command;
    }

    public string Command { get; }
    
    public string Parameters
    {
        get => this.parameters ?? string.Empty;
        init => this.parameters = value;
    }
}