using System;
using System.Runtime.InteropServices;

namespace FYA.PInvoke.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct SP_DEVINFO_DATA
    {
        #region Var
        public static SP_DEVINFO_DATA Empty { get; private set; }

        public readonly int Size;
        public readonly Guid ClassGuid;
        public readonly int DeviceInstance;
        public readonly IntPtr Reserved;
        #endregion

        #region Init
        static SP_DEVINFO_DATA() => Empty = new SP_DEVINFO_DATA(Marshal.SizeOf(typeof(SP_DEVINFO_DATA)));

        private SP_DEVINFO_DATA(int Size) : this() => this.Size = Size;

        public SP_DEVINFO_DATA(Guid ClassGuid, int DeviceInstance)
        {
            Size = Marshal.SizeOf(typeof(SP_DEVINFO_DATA));
            this.ClassGuid = ClassGuid;
            this.DeviceInstance = DeviceInstance;
            Reserved = IntPtr.Zero;
        }
        #endregion

        #region Functions
        public override string ToString() => $"{ClassGuid}: {DeviceInstance}";
        public override int GetHashCode() => ClassGuid.GetHashCode();
        public override bool Equals(object Obj) => Obj is SP_DEVINFO_DATA o && o.ClassGuid == ClassGuid && o.DeviceInstance == DeviceInstance; 
        #endregion
    }
}
