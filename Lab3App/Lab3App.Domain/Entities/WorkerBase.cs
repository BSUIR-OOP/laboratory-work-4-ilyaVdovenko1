using System.Data.Common;

namespace Lab3App.Domain.Entities;

public abstract class WorkerBase
{
    protected WorkerBase(ContactInformation contacts, Name name)
    {
        this.Contacts = contacts;
        this.Name = name;
        this.Id = Guid.NewGuid();
    }

    public Guid Id { get; }
    
    public Name Name { get; set; }
    
    public ContactInformation Contacts { get; set; }
    
    
}