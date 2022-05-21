using Lab3App.Domain.Enums;
using Lab3App.Domain.Interfaces;

namespace Lab3App.Domain.Entities;

public class Manager : OfficeWorker
{
    public ManagerTitles Title { get; set; }
    public Manager(ContactInformation contacts, Name name, string cityName, string officeAddressLine, ManagerTitles title) 
        : base(contacts, name, cityName, officeAddressLine)
    {
        this.Title = title;
    }
    
}