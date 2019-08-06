using System;

namespace FYA.PInvoke
{
    [Flags]
    public enum SetupDiGetClassDevsFlags
    {
        Default         = 0b00001,
        Present         = 0b00010,
        AllClasses      = 0b00100,
        Profile         = 0b01000,
        DeviceInterface = 0b10000
    }
}
