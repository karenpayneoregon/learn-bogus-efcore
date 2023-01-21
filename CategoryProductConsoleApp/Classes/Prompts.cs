using Spectre.Console;

namespace CategoryProductConsoleApp.Classes;

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
        
}