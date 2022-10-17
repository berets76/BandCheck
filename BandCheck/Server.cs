// Copyright: (c) Luca Beretta
// GNU General Public License v3.0+ (see COPYING or https://www.gnu.org/licenses/gpl-3.0.txt)

namespace BandCheck;

internal class Server
{
    internal void RunAsServer(List<string> Addresses)
    {
        try
        {
            Console.Clear();
            Console.WriteLine($"Starting as server, press Ctrl+C to quit{Environment.NewLine + Environment.NewLine}Binding to these addresses:");
            Console.WriteLine($"> {string.Join($"{Environment.NewLine}> ", Addresses)}");
            var threads = new List<Thread>();
            foreach (var address in Addresses)
            {
                Thread newThread = new(() => ServerListener(address))
                {
                    IsBackground = true,
                    Name = address
                };
                newThread.Start();
                threads.Add(newThread);
            }
            while (true)
            {
                // wait for Ctrl+C
            }
        }
        catch (Exception exc)
        {
            ConsoleHelper.PrintError(exc.Message);
        }
    }

    internal void ServerListener(string Address)
    {
        IPEndPoint ipEP = new(IPAddress.Parse(Address), Constants.TCP_PORT);

        while (true)
        {
            using Socket listener = new(ipEP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(ipEP);
            listener.Listen(10);
            Console.WriteLine($"Listening on {Address}");
            var handler = listener.Accept();
            Console.WriteLine($"{Environment.NewLine}Connected on {Address} from {handler.RemoteEndPoint}{Environment.NewLine}");
            long bytesReceived = 0;
            // by ref
            long bytesCount = 0;
            int StartTick = Environment.TickCount;
            int LastTick = Environment.TickCount;
            float Peek = 0;
            // Receive message
            var buffer = new byte[1024];
            long bytesRead = 0;
            while ((bytesRead = handler.Receive(buffer, SocketFlags.None)) > 0)
            {
                bytesReceived += bytesRead;
                ComputeHelper.ComputeCPS(bytesRead, ref LastTick, ref Peek, ref bytesCount);
            }
            listener.Close();
            float CPS = ((float)bytesReceived / ((float)(Environment.TickCount - StartTick)) * (float)1000);
            ConsoleHelper.PrintResults("Average", CPS);
            ConsoleHelper.PrintResults("Peek", Peek);
            Console.WriteLine($"{Environment.NewLine}Client disconnected: {bytesReceived,20:N2} Kb transferred in {(((float)(Environment.TickCount - StartTick)) / (float)1000)} seconds.{Environment.NewLine}");
        }
    }
}
