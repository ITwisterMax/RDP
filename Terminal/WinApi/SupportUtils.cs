using System;

namespace Rdp.Terminal.Core.WinApi
{
    // Поддержка RDP методов
    public static class SupportUtils
    {
        // Проверка ОС
        public static bool CheckOperationSytem()
        {
            // Получение версии ОС
            var osVersion = Environment.OSVersion;
            int major = osVersion.Version.Major;

            // Проверка на Windows Vista и выше
            return osVersion.Platform == PlatformID.Win32NT && major >= 6;
        }
    }
}