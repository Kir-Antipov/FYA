using System;
using System.Security;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Runtime.ConstrainedExecution;

namespace FYA.PInvoke.Structs
{
    public class SP_DEVINFO_HANDLE : SafeHandleZeroOrMinusOneIsInvalid
    {
        #region Imports
        [SuppressUnmanagedCodeSecurity]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(SetupAPI.ApiName, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        private static extern bool SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);
        #endregion

        #region Init
        public SP_DEVINFO_HANDLE() : base(true) { }
        #endregion

        #region Functions
        protected override bool ReleaseHandle() => SetupDiDestroyDeviceInfoList(handle);
        #endregion
    }
}
