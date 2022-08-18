using System;
using System.Threading.Tasks;
using EmployeesWithRelationsLibrary.Classes;

namespace EmployeesWithRelationsConsoleApp
{
    partial class Program
    {
        static async Task Main(string[] args)
        {
            var (success, exception) = await BogusOperations.CreateDatabaseAndPopulate(10);
        }
    }
}
