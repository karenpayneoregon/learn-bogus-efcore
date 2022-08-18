using System;
using System.Linq;
using System.Threading.Tasks;
using HasDataConsoleApp.Data;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace HasDataConsoleApp
{
    partial class Program
    {
        static async Task Main(string[] args)
        {
            AnsiConsole.MarkupLine("[b]Creating and populating[/]...");
            await using var context = new Context();
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            var products = await context.Product.Where(x => x.Id < 10).ToListAsync();

            var table = new Table()
                .RoundedBorder()
                .AddColumn("[b]Id[/]")
                .AddColumn("[b]Name[/]")
                .AddColumn("[b]Unit price[/]")
                .AddColumn("[b]Category[/]")
                .Alignment(Justify.Center)
                .BorderColor(Color.LightSlateGrey)
                .Title("[LightGreen]Products[/]");

            AnsiConsole.Clear();
            foreach (var product in products)
            {
                table.AddRow(product.Id.ToString(), product.ProductName, product.Price.Value.ToString("C2"), product.Category);
            }

            AnsiConsole.Write(table);
            Console.ReadLine();
        }
    }
}
