// Copyright: (c) Luca Beretta
// GNU General Public License v3.0+ (see COPYING or https://www.gnu.org/licenses/gpl-3.0.txt)

namespace BandCheck.Helpers;

internal class NetworkHelper
{
    internal static bool IsNetworkConnected()
    {
        try
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }
        catch (Exception exc)
        {
            ConsoleHelper.PrintError(exc.Message);
            return false;
        }
    }

    internal static List<string> GetAllAddresses(bool OnlyIPV4)
    {
        try
        {
            var addresses = new List<string>();
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (!OnlyIPV4 || (OnlyIPV4 && ip.AddressFamily == AddressFamily.InterNetwork))
                {
                    addresses.Add(ip.ToString());
                }
            }
            return addresses;
        }
        catch (Exception exc)
        {
            ConsoleHelper.PrintError(exc.Message);
            return null;
        }
    }
}
