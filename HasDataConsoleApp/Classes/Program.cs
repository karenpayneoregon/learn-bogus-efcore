using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HasDataConsoleApp.Classes;
using HasDataConsoleApp.Data;
using W = ConsoleHelperLibrary.Classes.WindowUtility;

// ReSharper disable once CheckNamespace
namespace HasDataConsoleApp
{
    partial class Program
    {
        [ModuleInitializer]
        public static void Init()
        {
            W.SetConsoleWindowPosition(W.AnchorWindow.Center);
            Console.Title = "EF Core/Bogus basic one table - HasData";
        }
        public static async Task<(bool success, Exception exception)> CheckDatabaseExists()
        {
            await using var context = new Context();

            var (success, exception) = await context.CanConnectAsync();
            return (success, exception);
        }
    }
}





