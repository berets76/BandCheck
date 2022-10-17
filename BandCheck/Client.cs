// Copyright: (c) Luca Beretta
// GNU General Public License v3.0+ (see COPYING or https://www.gnu.org/licenses/gpl-3.0.txt)

namespace BandCheck
{
    internal static class Client
    {
        internal static void RunAsClient(string TargetAddress)
        {
            try
            {
                Console.Clear();
                IPEndPoint ipEP = new(IPAddress.Parse(TargetAddress), Constants.TCP_PORT);
                using Socket connector = new(ipEP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                Console.WriteLine($"Connecting to {TargetAddress}");
                connector.Connect(ipEP);
                if (connector.Connected)
                {
                    var buffer = new byte[1024];
                    long kbToSend = Constants.MB_TO_SEND * 1024 * 1024;
                    long bytesTotalSent = 0;
                    long bytesSent = 0;
                    // by ref
                    long bytesCount = 0;
                    int StartTick = Environment.TickCount;
                    int LastTick = Environment.TickCount;
                    float Peek = 0;

                    while ((bytesSent = connector.Send(buffer, (int)(kbToSend - bytesTotalSent <= buffer.Length ? kbToSend - bytesTotalSent : buffer.Length), SocketFlags.None)) > 0)
                    {
                        bytesTotalSent += bytesSent;
                        ComputeHelper.ComputeCPS(bytesSent, ref LastTick, ref Peek, ref bytesCount);
                        if (bytesTotalSent >= kbToSend)
                            break;
                    }

                    Console.WriteLine("Disconnecting...");
                    connector.Close();
                    float CPS = ((float)bytesTotalSent / ((float)(Environment.TickCount - StartTick)) * (float)1000);
                    ConsoleHelper.PrintResults("Average", CPS);
                    ConsoleHelper.PrintResults("Peek", Peek);
                    Console.WriteLine($"{Environment.NewLine}Disconnected: {bytesTotalSent,20:N2} Kb transferred in {(((float)(Environment.TickCount - StartTick)) / (float)1000)} seconds.{Environment.NewLine}");
                }
            }
            catch (Exception exc)
            { ConsoleHelper.PrintError(exc.Message); }
        }
    }
}
