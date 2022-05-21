using System.ComponentModel.DataAnnotations;

namespace Lab3App.Domain.Entities;

public class ContactInformation
{
    [EmailAddress]
    public string Email { get; set; }
    
    [Phone]
    public string Phone { get; set; }
    
    
}