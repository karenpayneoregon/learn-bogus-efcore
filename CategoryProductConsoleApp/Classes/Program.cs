using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CategoriesProductsLibrary.Data;
using CategoryProductConsoleApp.Classes;
using SpectreConsoleHelperLibrary.Classes;
using W = ConsoleHelperLibrary.Classes.WindowUtility;


// ReSharper disable once CheckNamespace
namespace CategoryProductConsoleApp
{
    partial class Program
    {
        [ModuleInitializer]
        public static void Init()
        {
            W.SetConsoleWindowPosition(W.AnchorWindow.Center);
            Console.Title = "Categories/Products EF Core code sample";
            Panels.ShowHeader("Demo", "This application will create a new database defined by [cyan]EF Core[/] models with the [b]database[/] name in [b]appsettings.json[/]. You will be prompted to enter how many records to create then used to create Products followed by displaying the data.");
        }

        public static async Task<(bool success, Exception exception)> CheckDatabaseExists()
        {
            await using var context = new NorthWindContext();

            var (success, exception) = await context.CanConnectAsync();
            return (success, exception);
        }
    }
}





