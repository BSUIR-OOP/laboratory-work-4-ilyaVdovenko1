namespace LabApp3.Infrastructure.Interfaces;

public interface IRecordsServiceJsonWriter
{
    public void Write(Dictionary<string, string> data);
}