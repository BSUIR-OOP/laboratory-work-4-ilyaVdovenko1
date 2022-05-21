using System.Collections.ObjectModel;
using Lab3App.Application.Interfaces;
using Lab3App.Domain.Interfaces;

namespace Lab3App.Application.Models;

public class RecordsHandlerService : IRecordsHandlerService
{
    public int CreateRecord(UserRecordData data)
    {
        throw new NotImplementedException();
    }

    public void EditRecord(int id, UserRecordData data)
    {
        throw new NotImplementedException();
    }

    public ReadOnlyCollection<IRecordSnapshot> FindByFirstName(string firstName)
    {
        throw new NotImplementedException();
    }

    public ReadOnlyCollection<IRecordSnapshot> FindByLastName(string lastName)
    {
        throw new NotImplementedException();
    }

    public ReadOnlyCollection<IRecordSnapshot> GetRecords()
    {
        throw new NotImplementedException();
    }

    public int GetStat()
    {
        throw new NotImplementedException();
    }

    public IRecordsServiceSnapshot MakeSnapshot()
    {
        throw new NotImplementedException();
    }

    public void Restore(IRecordsServiceSnapshot snapshot)
    {
        throw new NotImplementedException();
    }

    public void Remove(int id)
    {
        throw new NotImplementedException();
    }
}