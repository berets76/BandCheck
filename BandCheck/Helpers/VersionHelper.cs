// Copyright: (c) Luca Beretta
// GNU General Public License v3.0+ (see COPYING or https://www.gnu.org/licenses/gpl-3.0.txt)

using System.Reflection;

namespace BandCheck.Helpers
{
    internal static class VersionHelper
    {
        internal static string GetVersion()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            return $"{version?.Major}.{version?.Minor} Build {version?.Build}.{version?.Revision}";
        }
    }
}
