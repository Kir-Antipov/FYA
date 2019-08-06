using System.Runtime.InteropServices;

namespace FYA.PInvoke.Structs
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public readonly struct DISPLAY_DEVICE
    {
        #region Var
        public static DISPLAY_DEVICE Empty { get; private set; }

        [MarshalAs(UnmanagedType.U4)]
        private readonly int BytesCount;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public readonly string DeviceName;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public readonly string DeviceString;

        [MarshalAs(UnmanagedType.U4)]
        public readonly DisplayDeviceStateFlags StateFlags;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public readonly string DeviceID;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public readonly string DeviceKey;
        #endregion

        #region Init
        static DISPLAY_DEVICE() => Empty = new DISPLAY_DEVICE(Marshal.SizeOf(typeof(DISPLAY_DEVICE)));

        private DISPLAY_DEVICE(int BytesCount) : this() => this.BytesCount = BytesCount;
        #endregion

        #region Functions
        public override string ToString() => DeviceString;
        public override int GetHashCode() => DeviceID?.GetHashCode() ?? -1;
        public override bool Equals(object Obj) => Obj is DISPLAY_DEVICE o && o.DeviceID == DeviceID && o.DeviceKey == DeviceKey;
        #endregion
    }
}
