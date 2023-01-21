using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CategoriesProductsLibrary.Classes;
using CategoriesProductsLibrary.models;
using CategoryProductConsoleApp.Classes;
using Spectre.Console;
using SpectreConsoleHelperLibrary.Classes;

namespace CategoryProductConsoleApp
{
    partial class Program
    {
        static async Task Main(string[] args)
        {

            if (Dialogs.AskConfirmation($"[lightgreen]Do you want to continue?[/]"))
            {
                var count = Prompts.GetInt();
                if (count >0)
                {
                    await Initialize(count);
                    await ReadProducts();                    
                }
            }
           
        }

        private static async Task ReadProducts()
        {
            AnsiConsole.Clear();

            List<Products> list = await DataOperations.GetProductsList();

            var table = new Table()
                .RoundedBorder()
                .AddColumn("[b]Id[/]")
                .AddColumn("[b]Name[/]")
                .AddColumn("[b]Unit price[/]")
                .AddColumn("[b]Category[/]")
                .Alignment(Justify.Center)
                .BorderColor(Color.LightSlateGrey)
                .Title("[LightGreen]Products[/]");


            foreach (var product in list)
            {
                // ReSharper disable once PossibleInvalidOperationException
                table.AddRow(product.ProductId.ToString(), product.ProductName, product.UnitPrice!.Value.ToString("C2"), product.Category.CategoryName);
            }

            AnsiConsole.Write(table);
            
            Console.WriteLine();
            AnsiConsole.MarkupLine("Press [b]any[/] key to leave");

            Console.ReadLine();

        }

        /// <summary>
        /// Create db and populate tables
        /// </summary>
        /// <returns></returns>
        private static async Task Initialize(int count)
        {
            AnsiConsole.MarkupLine("[skyblue1]Creating and populating database[/]");
            
            var (success, exception) = await BogusOperations.CreateDatabaseAndPopulate(count);

            if (!success)
            {
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[red]Failed to create and populated[/]");
                Console.WriteLine(exception.Message);
            }
        }

    }
}
