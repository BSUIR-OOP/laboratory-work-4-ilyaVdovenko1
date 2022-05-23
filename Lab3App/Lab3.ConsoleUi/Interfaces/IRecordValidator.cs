using Lab3App.Application.Models;

namespace Lab3.ConsoleUi.Interfaces;


public interface IRecordValidator
{
    public void ValidateParameters(UserRecordData parameters);
}