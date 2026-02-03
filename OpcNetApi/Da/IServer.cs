using System;

namespace Opc.Da
{
    public interface IServer : Opc.IServer, IDisposable
    {
        int GetResultFilters();

        void SetResultFilters(int filters);

        ServerStatus GetStatus();

        ItemValueResult[] Read(Item[] items);

        IdentifiedResult[] Write(ItemValue[] values);

        ISubscription CreateSubscription(SubscriptionState state);

        void CancelSubscription(ISubscription subscription);

        BrowseElement[] Browse(ItemIdentifier itemID, BrowseFilters filters, out BrowsePosition position);

        BrowseElement[] BrowseNext(ref BrowsePosition position);

        ItemPropertyCollection[] GetProperties(ItemIdentifier[] itemIDs, PropertyID[] propertyIDs, bool returnValues);
    }
}