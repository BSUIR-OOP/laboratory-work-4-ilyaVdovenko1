using Lab3App.Domain.Enums;
using Lab3App.Domain.Interfaces;

namespace Lab3App.Domain.Entities;

public class Developer : OfficeWorker, IWorker
{
    public DeveloperTitles Title { get; set; }
    
    public Developer(ContactInformation contacts, Name name, string cityName, string officeAddressLine, DeveloperTitles title) 
        : base(contacts, name, cityName, officeAddressLine)
    {
        this.Title = title;
    }
    
}