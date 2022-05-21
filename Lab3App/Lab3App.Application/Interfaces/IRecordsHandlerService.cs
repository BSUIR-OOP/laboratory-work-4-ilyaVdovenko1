using System.Collections.ObjectModel;
using Lab3App.Application.Models;
using Lab3App.Domain.Interfaces;

namespace Lab3App.Application.Interfaces;

public interface IRecordsHandlerService
{

    public int CreateRecord(UserRecordData data);
    
    public void EditRecord(int id, UserRecordData data);
    
    public ReadOnlyCollection<IRecordSnapshot> FindByFirstName(string firstName);
    
    public ReadOnlyCollection<IRecordSnapshot> FindByLastName(string lastName);

    public ReadOnlyCollection<IRecordSnapshot> GetRecords();

    public int GetStat();
    
    public IRecordsServiceSnapshot MakeSnapshot();
    
    public void Restore(IRecordsServiceSnapshot snapshot);

    public void Remove(int id);
    
}