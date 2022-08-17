using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CategoriesProductsLibrary.Classes;
using CategoriesProductsLibrary.models;
using Spectre.Console;

namespace CategoryProductConsoleApp
{
    partial class Program
    {
        static async Task Main(string[] args)
        {
            await Task.Delay(0);
            /*
             * MUST run this at least once to create the database and populate
             */
            //await Initialize();
            //await ReadProducts();
        }

        private static async Task ReadProducts()
        {
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
                table.AddRow(product.ProductId.ToString(), product.ProductName, product.UnitPrice.Value.ToString("C2"), product.Category.CategoryName);
            }

            AnsiConsole.Write(table);

            Console.ReadLine();
        }

        /// <summary>
        /// Create db and populate tables
        /// </summary>
        /// <returns></returns>
        static async Task Initialize()
        {
            AnsiConsole.MarkupLine("[yellow]Creating and populating database[/]");
            var (success, exception) = await BogusOperations.CreateDatabaseAndPopulate();
            if (success)
            {
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[cyan]Created and populated[/]");
            }
            else
            {
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[red]Failed to create and populated[/]");
            }

            AnsiConsole.MarkupLine("[yellow]Press a key to continue[/]");
            Console.ReadLine();

        }

    }
}
