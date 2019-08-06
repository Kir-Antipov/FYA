using System;

namespace FYA.PInvoke
{
    [Flags]
    public enum DisplayDeviceStateFlags
    {
        AttachedToDesktop   = 0b000001,
        MultiDriver         = 0b000010,
        PrimaryDevice       = 0b000100,
        MirroringDriver     = 0b001000,
        VGACompatible       = 0b010000,
        Removable           = 0b100000,
        ModesPruned         = 0b1000000000000000000000000000,
        Remote              = 0b0100000000000000000000000000,
        Disconnect          = 0b0010000000000000000000000000
    }
}
