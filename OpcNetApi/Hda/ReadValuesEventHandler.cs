using System;

namespace Opc.Hda
{
    public delegate void ReadValuesEventHandler(IRequest request, ItemValueCollection[] results);
}