using System;
using System.Threading.Tasks;
using EmployeesWithRelationsLibrary.Classes;
using SpectreConsoleHelperLibrary.Classes;

namespace EmployeesWithRelationsConsoleApp
{
    partial class Program
    {
        static async Task Main(string[] args)
        {
            Panels.ShowHeader("Header", "Some [b]text[/] here");

            await Task.Delay(0);
            //var (success, exception) = await BogusOperations.CreateDatabaseAndPopulate(10);
            Console.ReadLine();
        }
    }
}
