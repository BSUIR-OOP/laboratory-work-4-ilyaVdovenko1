using Lab3App.Domain.Enums;
using Lab3App.Domain.Interfaces;

namespace Lab3App.Domain.Entities;

public class Developer : OfficeWorker, IWorker
{
    public string Title { get; set; }
    
    public Developer(ContactInformation contacts, Name name, string cityName, string officeAddressLine, string title) 
        : base(contacts, name, cityName, officeAddressLine)
    {
        this.Title = title;
        this.Position = nameof(Developer);
        this.WorkerId = Id;
    }
    
    public Developer() : base()
    {
        this.Position = nameof(Developer);
        this.WorkerId = Id;
    }


    public string Position { get; set; }
    public Guid WorkerId { get; }
}