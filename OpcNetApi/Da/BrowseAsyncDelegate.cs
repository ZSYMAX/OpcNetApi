using System;

namespace Opc.Da
{
    public delegate BrowseElement[] BrowseAsyncDelegate(ItemIdentifier itemID, BrowseFilters filters, out BrowsePosition position);
}