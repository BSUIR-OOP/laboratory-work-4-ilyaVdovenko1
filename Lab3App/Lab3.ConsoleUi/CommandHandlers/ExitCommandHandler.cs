using Lab3App.Application.Models;

namespace Lab3.ConsoleUi.CommandHandlers;

public class ExitCommandHandler : CommandHandlerBase
{
    private readonly Action<bool> appStatusChanger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExitCommandHandler"/> class.
    /// </summary>
    /// <param name="appStatusChanger">Defines if app working.</param>
    public ExitCommandHandler(Action<bool> appStatusChanger)
    {
        this.appStatusChanger = appStatusChanger;
    }

    protected override string HandlerName { get; set; } = "exit";

    public override void Handle(AppCommandRequest commandRequest)
    {
        if (!string.Equals(HandlerName, commandRequest.Command, StringComparison.InvariantCultureIgnoreCase))
        {
            base.Handle(commandRequest);
        }
        this.Exit();
    }
    private void Exit()
    {
        this.appStatusChanger.Invoke(false);
        Console.WriteLine("Exiting an application...");
    }
}