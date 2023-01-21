using HasDataConsoleApp.Data;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using SpectreConsoleHelperLibrary.Classes;

namespace HasDataConsoleApp;

partial class Program
{
    static async Task Main(string[] args)
    {
        Panels.ShowHeader("Demo","Create or recreates our database followed by populating data then displaying data.");

        await using var context = new Context();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        var products = await context.Product.ToListAsync();

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
            // ReSharper disable once PossibleInvalidOperationException
            table.AddRow(product.Id.ToString(), product.ProductName, product.Price!.Value.ToString("C2"), product.Category);
        }

        AnsiConsole.Write(table);

        Console.WriteLine();
        AnsiConsole.MarkupLine("Press [b]any[/] key to leave");
        Console.ReadLine();
    }
}