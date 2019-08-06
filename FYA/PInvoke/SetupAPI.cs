using System;
using System.Linq;
using System.Text;
using System.Security;
using FYA.PInvoke.Structs;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FYA.PInvoke
{
    public static class SetupAPI
    {
        #region Imports
        public const string ApiName = "setupapi.dll";

        [DllImport(ApiName, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        private static extern bool SetupDiCallClassInstaller(DiFunction InstallFunction, SP_DEVINFO_HANDLE DeviceInfoSet, ref SP_DEVINFO_DATA DeviceInfoData);

        [DllImport(ApiName, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        private static extern bool SetupDiEnumDeviceInfo(SP_DEVINFO_HANDLE DeviceInfoSet, int MemberIndex, ref SP_DEVINFO_DATA DeviceInfoData);

        [DllImport(ApiName, CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern SP_DEVINFO_HANDLE SetupDiGetClassDevs(ref Guid classGuid, [MarshalAs(UnmanagedType.LPWStr)]string Enumerator, IntPtr HwndParent, SetupDiGetClassDevsFlags Flags);

        [DllImport(ApiName, SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool SetupDiGetDeviceInstanceId(SP_DEVINFO_HANDLE DeviceInfoSet, in SP_DEVINFO_DATA DeviceInfoData, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder DeviceInstanceId, int DeviceInstanceIdSize, out int RequiredSize);

        [DllImport(ApiName, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        private static extern bool SetupDiSetClassInstallParams(SP_DEVINFO_HANDLE DeviceInfoSet, ref SP_DEVINFO_DATA DeviceInfoData, ref SP_CLASS_PARAMS ClassInstallParams, int ClassInstallParamsSize);
        #endregion

        #region Wrappers
        public static void ChangeDeviceState(Guid ClassGuid, string InstanceID, bool Enabled)
        {
            using (SP_DEVINFO_HANDLE setHandle = SetupDiGetClassDevs(ref ClassGuid, null, IntPtr.Zero, SetupDiGetClassDevsFlags.Present))
            {
                var deviceData = EnumerateDevices(setHandle)
                    .Select(x => new { Device = x, InstanceID = GetDeviceInstanceID(setHandle, x) })
                    .Where(x => !string.IsNullOrEmpty(x.InstanceID) && x.InstanceID.Contains(InstanceID))
                    .OrderByDescending(x => x.InstanceID.Length)
                    .FirstOrDefault();
                if (deviceData == null)
                    throw new Win32Exception((int)WinApiError.DevinfoNotRegistered, "The specified device was not found");
                ChangeDeviceState(setHandle, deviceData.Device, Enabled);
            }
        }

        public static IEnumerable<SP_DEVINFO_DATA> EnumerateDevices(SP_DEVINFO_HANDLE Handle)
        {
            SP_DEVINFO_DATA device = SP_DEVINFO_DATA.Empty;
            for (int i = 0; SetupDiEnumDeviceInfo(Handle, i, ref device); ++i)
                yield return device;
        }

        public static string GetDeviceInstanceID(SP_DEVINFO_HANDLE SetHandle, in SP_DEVINFO_DATA Device)
        {
            StringBuilder builder = new StringBuilder(128);
            bool result = SetupDiGetDeviceInstanceId(SetHandle, Device, builder, builder.Capacity, out int requiredSize);
            if (!result && User32.LastError == WinApiError.InsufficientBuffer)
            {
                builder.Capacity = requiredSize;
                result = SetupDiGetDeviceInstanceId(SetHandle, Device, builder, builder.Capacity, out requiredSize);
            }
            return result ? builder.ToString() : null;
        }

        public static void ChangeDeviceState(SP_DEVINFO_HANDLE SetHandle, SP_DEVINFO_DATA Device, bool Enabled)
        {
            SP_CLASS_PARAMS parameters = new SP_CLASS_PARAMS(Scopes.Global, DiFunction.PropertyChange, Enabled ? StateChangeAction.Enable : StateChangeAction.Disable);
            bool success = SetupDiSetClassInstallParams(SetHandle, ref Device, ref parameters, Marshal.SizeOf(parameters));
            if (success)
                success = SetupDiCallClassInstaller(DiFunction.PropertyChange, SetHandle, ref Device);
            if (!success)
                switch (User32.LastError)
                {
                    case WinApiError.NotDisableable:
                        throw new ArgumentException("Device isn't disableable");
                    case WinApiError.AccessDenied:
                        throw new SecurityException("Not enough rights to complete the operation");
                    default:
                        throw new Win32Exception(User32.LastError.ToString());
                }
        }
        #endregion
    }
}
