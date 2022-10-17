// Copyright: (c) Luca Beretta
// GNU General Public License v3.0+ (see COPYING or https://www.gnu.org/licenses/gpl-3.0.txt)

namespace BandCheck.Helpers;

internal class ConsoleHelper
{
    internal static void PrintStart()
    {
        Console.Clear();
        Console.WriteLine($"### BandCheck - Version {VersionHelper.GetVersion()}{Environment.NewLine}");
    }
    internal static void PrintResults(string Item, float Value)
    {
        Console.WriteLine($"{Item,15} CPS: {Value,20:N2}  KPS: {Value / 1024,20:N2}  MPS: {(Value / 1024) / 1024,20:N2}");
    }
    internal static void PrintError(string Message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"# {Message}");
        Console.ResetColor();
    }
}
