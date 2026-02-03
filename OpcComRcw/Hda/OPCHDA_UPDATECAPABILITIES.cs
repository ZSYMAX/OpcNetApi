using System;

namespace OpcRcw.Hda
{
    public enum OPCHDA_UPDATECAPABILITIES
    {
        OPCHDA_INSERTCAP = 1,
        OPCHDA_REPLACECAP,
        OPCHDA_INSERTREPLACECAP = 4,
        OPCHDA_DELETERAWCAP = 8,
        OPCHDA_DELETEATTIMECAP = 16
    }
}