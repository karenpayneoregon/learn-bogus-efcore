using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace EmployeesWithRelationsConsoleApp.Classes
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
    }
}
