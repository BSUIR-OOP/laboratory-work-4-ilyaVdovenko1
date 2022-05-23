using Lab3App.Application.Interfaces;
using Lab3App.Application.Models;
using Lab3App.Domain.Interfaces;

namespace Lab3.ConsoleUi.CommandHandlers;

public abstract class CommandHandlersWithSnapshot : ServiceCommandHandlerBase
{

    protected readonly IRecordSnapshot Snapshot;
    protected CommandHandlersWithSnapshot(IRecordsHandlerService service, IRecordSnapshot snapshot)
        : base(service)
    {
        Snapshot = snapshot;
    }

    


    protected static T ReadInput<T>(Func<string, Tuple<bool, string, T>> converter, Func<T, Tuple<bool, string>> validator)
    {
        do
        {
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Field can not be empty. Please, correct your input.");
                continue;
            }

            var conversionResult = converter(input);

            if (!conversionResult.Item1)
            {
                Console.WriteLine($"Conversion failed: {conversionResult.Item2}. Please, correct your input.");
                continue;
            }

            var value = conversionResult.Item3;

            var validationResult = validator(value);
            if (validationResult.Item1) return value;
            Console.WriteLine($"Validation failed: {validationResult.Item2}. Please, correct your input.");

        }
        
        while (true);
    }
    
    protected static T ReadInput<T>(Func<string, Tuple<bool, string, T>> converter)
    {
        do
        {
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Field can not be empty. Please, correct your input.");
                continue;
            }

            var conversionResult = converter(input);

            if (!conversionResult.Item1)
            {
                Console.WriteLine($"Conversion failed: {conversionResult.Item2}. Please, correct your input.");
                continue;
            }

            var value = conversionResult.Item3;

            
            return value;

        }
        
        while (true);
    }
    
    protected  UserRecordData ReadInput(IWorker worker)
    {
        var properties = Snapshot.GetProperties(worker);
        var data = new Dictionary<string, object?>();
        
        foreach (var property in properties)
        {
            if (!property.CanWrite) continue;
            if (string.Equals(property.Name, "Position", StringComparison.InvariantCultureIgnoreCase))
            {
                data.Add(property.Name, worker.Position);
                continue;
            }
            Console.Write($"{property.Name}: ");
            var input = Console.ReadLine();
            data.Add(property.Name, input);

        }

        return new UserRecordData(worker.Position, data);
    }


    protected static Tuple<bool, string, string> StringConverter(string stringToConvert) =>
        new (!string.IsNullOrEmpty(stringToConvert), stringToConvert, stringToConvert);
    

    protected Tuple<bool, string, object> CustomConverter(string valueToConvert, Type typeToConvert)
    {
        var convertedType = Convert.ChangeType(valueToConvert, typeToConvert);

        return new Tuple<bool, string, dynamic>(true, valueToConvert, convertedType);

    }
    

    
}