using Lab3App.Domain.Interfaces;

namespace Lab3App.Application.Models;

public class UserRecordData
{
    public string Position { get; }
    public Dictionary<string, object?> Data { get; }
    public UserRecordData(string position, Dictionary<string, object?> data)
    {
        this.Position = position;
        this.Data = data;
    }
}