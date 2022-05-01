using System;

namespace Rdp.Terminal.Core.WinApi
{
    /// <summary>
    ///     Support class for OS check
    /// </summary>
    public static class SupportUtils
    {
        /// <summary>
        ///     OS should be Windows Vista +
        /// </summary>
        ///
        /// <returns>bool</returns>
        public static bool CheckOperationSytem()
        {
            var osVersion = Environment.OSVersion;
            int major = osVersion.Version.Major;

            return osVersion.Platform == PlatformID.Win32NT && major >= 6;
        }
    }
}