using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace SpectreConsoleHelperLibrary.Classes
{
    public class Dialogs
    {
        public static bool AskConfirmation(string text) => AnsiConsole.Confirm(text);
    }
}
