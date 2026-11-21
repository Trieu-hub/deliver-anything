using System;

namespace DeliverAnything.Core
{
    [Flags]
    public enum HazardFlags
    {
        None = 0,
        Fire = 1 << 0,
        Explosive = 1 << 1,
        Bite = 1 << 2
    }
}
