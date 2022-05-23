using Lab3App.Domain.Interfaces;

namespace Lab3App.Domain.Entities;

public abstract class OfficeWorker : WorkerBase
{
    protected OfficeWorker(ContactInformation contacts, Name name, string cityName, string officeAddressLine) : base(contacts, name)
    {
        this.City = cityName;
        this.OfficeAddressLine = officeAddressLine;
    }

    protected OfficeWorker() : base()
    {
        
    }
    
    public string City { get; set; }
    public string OfficeAddressLine { get; set; }
    
}