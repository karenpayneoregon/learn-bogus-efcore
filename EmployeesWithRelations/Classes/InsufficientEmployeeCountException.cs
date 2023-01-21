namespace EmployeesWithRelationsLibrary.Classes;

public class InsufficientEmployeeCountException : Exception
{
    public InsufficientEmployeeCountException() { }
    public InsufficientEmployeeCountException(string message) : base(message) { }
    public InsufficientEmployeeCountException(string message, Exception inner) : base(message, inner) { }
}