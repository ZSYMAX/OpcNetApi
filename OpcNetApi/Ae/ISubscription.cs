using System;

namespace Opc.Ae
{
    public interface ISubscription : IDisposable
    {
        event EventChangedEventHandler EventChanged;

        SubscriptionState GetState();

        SubscriptionState ModifyState(int masks, SubscriptionState state);

        SubscriptionFilters GetFilters();

        void SetFilters(SubscriptionFilters filters);

        int[] GetReturnedAttributes(int eventCategory);

        void SelectReturnedAttributes(int eventCategory, int[] attributeIDs);

        void Refresh();

        void CancelRefresh();
    }
}