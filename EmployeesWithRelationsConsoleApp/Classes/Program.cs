using System.Runtime.CompilerServices;
using EmployeesWithRelationsConsoleApp.Classes;
using EmployeesWithRelationsLibrary.Data;
using W = ConsoleHelperLibrary.Classes.WindowUtility;

// ReSharper disable once CheckNamespace
namespace EmployeesWithRelationsConsoleApp;

partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        W.SetConsoleWindowPosition(W.AnchorWindow.Center);
        Console.Title = "Employees EF Core code sample";
    }

    public static async Task<(bool success, Exception exception)> CheckDatabaseExists()
    {
        await using var context = new NorthWindContext();

        var (success, exception) = await context.CanConnectAsync();
        return (success, exception);
    }
}