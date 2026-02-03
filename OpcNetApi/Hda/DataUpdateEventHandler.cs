using System;

namespace Opc.Hda
{
    public delegate void DataUpdateEventHandler(IRequest request, ItemValueCollection[] results);
}