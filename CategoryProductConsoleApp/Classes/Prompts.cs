using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace CategoryProductConsoleApp.Classes
{
    class Prompts
    {
        /// <summary>
        /// Get an int with validation rather than the default validation message
        /// </summary>
        public static int GetInt() =>
            AnsiConsole.Prompt(
                new TextPrompt<int>("[yellow]Enter product count to create or[/] [b]0[/][yellow] to abort[/]")
                    .PromptStyle("cyan")
                    .DefaultValue(10)
                    .ValidationErrorMessage("[red]That's not a valid number[/]"));

        public static bool AskConfirmation(string text) => AnsiConsole.Confirm(text);

        public static void PanelBorders()
        {
            static IRenderable CreatePanel(string headerText, BoxBorder border)
            {
                return new Panel($"This application will create a new database defined by [cyan]EF Core[/] models with the [b]database[/] name in [b]appsettings.json[/]. You will be prompted to enter how many records to create then used to create Products followed by displaying the data.")
                    .Header($" [cyan]{headerText}[/] ", Justify.Center)
                    .Border(border)
                    .BorderStyle(Style.Parse("grey"))
                    .HeaderAlignment(Justify.Center);
            }

            var items = new[]
            {
                CreatePanel("About", BoxBorder.Square),
            };

            AnsiConsole.Write(
                new Padder(
                    new Columns(items).PadRight(1),
                    new Padding(2, 0, 20, 0)));
        }
    }
}
