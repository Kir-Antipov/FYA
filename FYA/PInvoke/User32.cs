using FYA.Models;
using System.Linq;
using FYA.PInvoke.Structs;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FYA.PInvoke
{
    public static class User32
    {
        #region Imports
        public const string ApiName = "user32.dll";

#if ANSI
        [DllImport(ApiName, EntryPoint = "EnumDisplayDevices")]
#else
        [DllImport(ApiName, EntryPoint = "EnumDisplayDevicesW")]
#endif
        private static extern bool EnumDisplayDevices(string DeviceID, [MarshalAs(UnmanagedType.U4)]int DeviceIndex, ref DISPLAY_DEVICE DisplayDevice, int Flags);
        #endregion

        #region Wrappers
        public static WinApiError LastError => (WinApiError)Marshal.GetLastWin32Error();

        public static IEnumerable<DISPLAY_DEVICE> EnumerateDisplayDevices()
        {
            DISPLAY_DEVICE device = DISPLAY_DEVICE.Empty;
            for (int i = 0; EnumDisplayDevices(null, i, ref device, 0); ++i)
                yield return device;
        }

        public static IEnumerable<VideoCard> EnumerateVideoCards() => EnumerateDisplayDevices().Where(x => !string.IsNullOrEmpty(x.DeviceID)).GroupBy(x => x.DeviceID).Select(VideoCard.FromDisplayDevices);
        #endregion
    }
}
