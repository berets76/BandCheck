// Copyright: (c) Luca Beretta
// GNU General Public License v3.0+ (see COPYING or https://www.gnu.org/licenses/gpl-3.0.txt)

namespace BandCheck.Helpers
{
    internal static class ComputeHelper
    {
        internal static bool ComputeCPS(long Bytes, ref int LastTick, ref float Peek, ref long BytesCount)
        {
            int currentTick = Environment.TickCount;

            BytesCount += Bytes;

            if ((currentTick - LastTick) < 1000)
                return false;

            float CPS = ((float)BytesCount / ((float)(currentTick - LastTick)) * (float)1000);
            ConsoleHelper.PrintResults("--->", CPS);
            if (CPS > Peek)
                Peek = CPS;

            BytesCount = 0;

            LastTick = currentTick;
            return true;
        }
    }
}
