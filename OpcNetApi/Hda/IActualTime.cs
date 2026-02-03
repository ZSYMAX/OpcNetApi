using System;

namespace Opc.Hda
{
    public interface IActualTime
    {
        DateTime StartTime { get; set; }

        DateTime EndTime { get; set; }
    }
}