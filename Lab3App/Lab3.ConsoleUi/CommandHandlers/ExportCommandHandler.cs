using System.Text;
using Lab3App.Application.Enums;
using Lab3App.Application.Interfaces;
using Lab3App.Application.Models;

namespace Lab3.ConsoleUi.CommandHandlers;

public class ExportCommandHandler : ServiceCommandHandlerBase
{
    private const string JsonType = "json";

    public ExportCommandHandler(IRecordsHandlerService service)
        : base(service)
    {
    }
    
    protected override string HandlerName { get; set; } = "export";

    public override void Handle(AppCommandRequest commandRequest)
    {
        if (!string.Equals(HandlerName, commandRequest.Command, StringComparison.InvariantCultureIgnoreCase))
        {
            base.Handle(commandRequest);
        }
        this.Export(commandRequest.Parameters);
    }
    private void Export(string parameters)
    {
        var argc = parameters.Split(" ");
        if (argc.Length != 2)
        {
            Console.WriteLine("Wrong number of arguments");
            return;
        }

        var extension = argc[0] switch
        {
            JsonType when argc[0].Equals(JsonType, StringComparison.OrdinalIgnoreCase) => FileTypes.Json,
            _ => throw new InvalidOperationException("Can not recognize file type."),
        };
        var path = argc[1];
        var dirPath = Path.GetDirectoryName(path);

        if (string.IsNullOrEmpty(path))
        {
            Console.WriteLine("Path is empty");
            return;
        }

        if (!Directory.Exists(dirPath))
        {
            Console.WriteLine($"Directory {dirPath} does not exists, try another path!");
            return;
        }

        while (File.Exists(path))
        {
            Console.Write($"File is exists - rewrite {path}? [Y/n] ");
            var answer = Console.ReadLine();
            if (answer != null && answer.Equals("y", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            if (answer != null && answer.Equals("n", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
        }

        try
        {
            this.Export(extension, path);
        }
        catch (ArgumentOutOfRangeException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private void Export(FileTypes extension, string path)
    {
        try
        {
            var nameOfFile = Path.GetFileName(path);

            var options = new FileStreamOptions
            {
                Access = FileAccess.Write,
                Mode = FileMode.Create,
            };
            using (var writer = new StreamWriter(path, Encoding.UTF8, options))
            {
                var snapshot = this.Service.MakeSnapshot();
                switch (extension)
                {
                    case FileTypes.Json:
                        snapshot.SaveToJson(writer);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(extension), extension, "Could not recognize file type.");
                }
            }

            Console.WriteLine($"All records are exported to file {nameOfFile}.");
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Please try again!");
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Please try again!");
        }
        catch (ObjectDisposedException)
        {
            Console.WriteLine("Oops, smth went wrong!");
            Console.WriteLine("Please try again!");
        }
        catch (IOException)
        {
            Console.WriteLine("Oops, smth went wrong!");
            Console.WriteLine("Please try again!");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("It seems you dont have permission to create files in that directory.");
            Console.WriteLine("Please try again!");
        }
    }
}