// Copyright: (c) Luca Beretta
// GNU General Public License v3.0+ (see COPYING or https://www.gnu.org/licenses/gpl-3.0.txt)

global using System.Net.NetworkInformation;
global using System.Net;
global using System.Net.Sockets;
global using System.Text;
global using BandCheck.Helpers;
global using BandCheck;

if (NetworkHelper.IsNetworkConnected())
{
    var addresses = NetworkHelper.GetAllAddresses(true);
    if (addresses != null && addresses.Count > 0)
    {
        ConsoleHelper.PrintStart();
        if (args.Count() == 0)
        {
            #region menu mode
            Console.WriteLine($"S - Start in server mode");
            Console.WriteLine($"C - Start in client mode");
            Console.WriteLine($"{Environment.NewLine}");
            Console.WriteLine($"Q - Close BandCheck");
            Console.WriteLine($"{Environment.NewLine}");
            var selection = Console.ReadLine();
            switch (selection?.ToLower())
            {
                case "s":
                    new Server().RunAsServer(addresses);
                    break;
                case "c":
                    Console.WriteLine("Type the IPV4 address of the server endpoint");
                    var address = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(address))
                        Client.RunAsClient(address);
                    else
                        return -3;
                    break;
                case "q":
                    return 0;
                default: return 0;
            }
            #endregion
        }
        else
        {
            #region parameter mode
            switch (args[0])
            {
                case "-s":
                    // server mode
                    new Server().RunAsServer(addresses);
                    break;
                case "-t":
                    // client mode pointing server address
                    if (!string.IsNullOrWhiteSpace(args[1]))
                        Client.RunAsClient(args[1]);
                    else
                        return -3;
                    break;
                default:
                    break;
            }
            #endregion
        }

        return 0;
    }
    else
    {
        ConsoleHelper.PrintError("No IPV4 network adapter found");
        return -2;
    }
}
else
{
    ConsoleHelper.PrintError("You are not connected to any network");
    return -1;
}