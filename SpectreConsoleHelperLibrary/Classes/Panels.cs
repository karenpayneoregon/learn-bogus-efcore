
using Spectre.Console;
using Spectre.Console.Rendering;

namespace SpectreConsoleHelperLibrary.Classes
{
    public class Panels
    {
        /// <summary>
        /// Introduction to a code sample
        /// </summary>
        /// <param name="headerText">header text for panel</param>
        /// <param name="contents">text to display in panel</param>
        public static void ShowHeader(string headerText, string contents)
        {
            static IRenderable CreatePanel(string headerText, string contents)
            {
                return new Panel(contents)
                    .Header($" [cyan]{headerText}[/] ", Justify.Center)
                    .Border(BoxBorder.Square)
                    .BorderStyle(Style.Parse("grey"))
                    .HeaderAlignment(Justify.Center);
            }

            var items = new[]
            {
                CreatePanel(headerText, contents),
            };

            AnsiConsole.Write(
                new Padder(
                    new Columns(items).PadRight(1),
                    new Padding(2, 0, 20, 0)));
        }
    }
}
