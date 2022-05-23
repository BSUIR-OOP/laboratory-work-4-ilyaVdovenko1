using Lab3App.Domain.Enums;
using Lab3App.Domain.Interfaces;

namespace Lab3App.Domain.Entities;

public class Manager : OfficeWorker, IWorker
{
    public string Title { get; set; }
    public Manager(ContactInformation contacts, Name name, string cityName, string officeAddressLine, string title) 
        : base(contacts, name, cityName, officeAddressLine)
    {
        this.Title = title;
        this.WorkerId = Id;
        this.Position = nameof(Manager);
    }

    public Manager()
    {
        this.Position = nameof(Manager);
        this.WorkerId = Id;
    }

    public string Position { get; set; }
    public Guid WorkerId { get; }
}