using System;

namespace Opc.Da
{
    public interface ISubscription : IDisposable
    {
        event DataChangedEventHandler DataChanged;

        int GetResultFilters();

        void SetResultFilters(int filters);

        SubscriptionState GetState();

        SubscriptionState ModifyState(int masks, SubscriptionState state);

        ItemResult[] AddItems(Item[] items);

        ItemResult[] ModifyItems(int masks, Item[] items);

        IdentifiedResult[] RemoveItems(ItemIdentifier[] items);

        ItemValueResult[] Read(Item[] items);

        IdentifiedResult[] Write(ItemValue[] items);

        IdentifiedResult[] Read(Item[] items, object requestHandle, ReadCompleteEventHandler callback, out IRequest request);

        IdentifiedResult[] Write(ItemValue[] items, object requestHandle, WriteCompleteEventHandler callback, out IRequest request);

        void Cancel(IRequest request, CancelCompleteEventHandler callback);

        void Refresh();

        void Refresh(object requestHandle, out IRequest request);

        void SetEnabled(bool enabled);

        bool GetEnabled();
    }
}