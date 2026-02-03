using System;

namespace Opc.Ae
{
    public delegate void EventChangedEventHandler(EventNotification[] notifications, bool refresh, bool lastRefresh);
}