using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeesWithRelationsConsoleApp.Classes;
using EmployeesWithRelationsLibrary.Classes;
using EmployeesWithRelationsLibrary.Models;
using Spectre.Console;
using SpectreConsoleHelperLibrary.Classes;

namespace EmployeesWithRelationsConsoleApp
{
    partial class Program
    {
        static async Task Main(string[] args)
        {
            if (Dialogs.AskConfirmation($"[lightgreen]Do you want to continue?[/]"))
            {
                AnsiConsole.Clear();

                var count = Prompts.GetInt();

                AnsiConsole.Clear();
                
                if (count > 0)
                {
                    Panels.ShowHeader("Working", "Creating and populating");
                    var (success, exception) = await BogusOperations.CreateDatabaseAndPopulate(count);

                    AnsiConsole.Clear();

                    if (success)
                    {
                        List<Employees> list = await BogusOperations.ReadEmployeesList();

                        var table = new Table()
                            .RoundedBorder()
                            .AddColumn("[b]Id[/]")
                            .AddColumn("[b]First[/]")
                            .AddColumn("[b]Last[/]")
                            .AddColumn("[b]Birth date[/]")
                            .Alignment(Justify.Center)
                            .BorderColor(Color.LightSlateGrey)
                            .Title("[LightGreen]Employees[/]");

                        foreach (var emp in list)
                        {
                            // ReSharper disable once PossibleInvalidOperationException
                            table.AddRow(emp.EmployeeID.ToString(), emp.FirstName, emp.LastName, emp.BirthDate.Value.ToString("d"));
                        }

                        AnsiConsole.Write(table);

                    }
                    else
                    {
                        AnsiConsole.MarkupLine($"Error: {exception.Message}");
                    }
                }
            }
            
            
            Console.WriteLine();
            AnsiConsole.MarkupLine("Press [b]any[/] key to leave");

            Console.ReadLine();
        }
    }
}
