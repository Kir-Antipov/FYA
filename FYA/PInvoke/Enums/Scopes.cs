using System;

namespace FYA.PInvoke
{
    [Flags]
    public enum Scopes
    {
        Global          = 0b001,
        ConfigSpecific  = 0b010,
        ConfigGeneral   = 0b100
    }
}
