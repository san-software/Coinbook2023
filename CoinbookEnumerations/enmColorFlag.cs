using System;

namespace Coinbook.Enumerations
{
    [Flags]
    public enum enmColorFlag
    {
        None = 0,
        S = 1,
        SP = 2,
        SS = 4,
        SSP = 8,
        VZ = 16,
        VZP=32,
        STN=64,
        STH=128,
        PP=256
    }
}
