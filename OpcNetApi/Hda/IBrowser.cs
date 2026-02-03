using System;

namespace Opc.Hda
{
    public interface IBrowser : IDisposable
    {
        BrowseFilterCollection Filters { get; }

        BrowseElement[] Browse(ItemIdentifier itemID);

        BrowseElement[] Browse(ItemIdentifier itemID, int maxElements, out IBrowsePosition position);

        BrowseElement[] BrowseNext(int maxElements, ref IBrowsePosition position);
    }
}