using System;

namespace Opc.Hda
{
    public interface IServer : Opc.IServer, IDisposable
    {
        ServerStatus GetStatus();

        Attribute[] GetAttributes();

        Aggregate[] GetAggregates();

        IBrowser CreateBrowser(BrowseFilter[] filters, out ResultID[] results);

        IdentifiedResult[] CreateItems(ItemIdentifier[] items);

        IdentifiedResult[] ReleaseItems(ItemIdentifier[] items);

        IdentifiedResult[] ValidateItems(ItemIdentifier[] items);

        ItemValueCollection[] ReadRaw(Time startTime, Time endTime, int maxValues, bool includeBounds, ItemIdentifier[] items);

        IdentifiedResult[] ReadRaw(Time startTime, Time endTime, int maxValues, bool includeBounds, ItemIdentifier[] items, object requestHandle, ReadValuesEventHandler callback, out IRequest request);

        IdentifiedResult[] AdviseRaw(Time startTime, decimal updateInterval, ItemIdentifier[] items, object requestHandle, DataUpdateEventHandler callback, out IRequest request);

        IdentifiedResult[] PlaybackRaw(Time startTime, Time endTime, int maxValues, decimal updateInterval, decimal playbackDuration, ItemIdentifier[] items, object requestHandle, DataUpdateEventHandler callback, out IRequest request);

        ItemValueCollection[] ReadProcessed(Time startTime, Time endTime, decimal resampleInterval, Item[] items);

        IdentifiedResult[] ReadProcessed(Time startTime, Time endTime, decimal resampleInterval, Item[] items, object requestHandle, ReadValuesEventHandler callback, out IRequest request);

        IdentifiedResult[] AdviseProcessed(Time startTime, decimal resampleInterval, int numberOfIntervals, Item[] items, object requestHandle, DataUpdateEventHandler callback, out IRequest request);

        IdentifiedResult[] PlaybackProcessed(Time startTime, Time endTime, decimal resampleInterval, int numberOfIntervals, decimal updateInterval, Item[] items, object requestHandle, DataUpdateEventHandler callback, out IRequest request);

        ItemValueCollection[] ReadAtTime(DateTime[] timestamps, ItemIdentifier[] items);

        IdentifiedResult[] ReadAtTime(DateTime[] timestamps, ItemIdentifier[] items, object requestHandle, ReadValuesEventHandler callback, out IRequest request);

        ModifiedValueCollection[] ReadModified(Time startTime, Time endTime, int maxValues, ItemIdentifier[] items);

        IdentifiedResult[] ReadModified(Time startTime, Time endTime, int maxValues, ItemIdentifier[] items, object requestHandle, ReadValuesEventHandler callback, out IRequest request);

        ItemAttributeCollection ReadAttributes(Time startTime, Time endTime, ItemIdentifier item, int[] attributeIDs);

        ResultCollection ReadAttributes(Time startTime, Time endTime, ItemIdentifier item, int[] attributeIDs, object requestHandle, ReadAttributesEventHandler callback, out IRequest request);

        AnnotationValueCollection[] ReadAnnotations(Time startTime, Time endTime, ItemIdentifier[] items);

        IdentifiedResult[] ReadAnnotations(Time startTime, Time endTime, ItemIdentifier[] items, object requestHandle, ReadAnnotationsEventHandler callback, out IRequest request);

        ResultCollection[] InsertAnnotations(AnnotationValueCollection[] items);

        IdentifiedResult[] InsertAnnotations(AnnotationValueCollection[] items, object requestHandle, UpdateCompleteEventHandler callback, out IRequest request);

        ResultCollection[] Insert(ItemValueCollection[] items, bool replace);

        IdentifiedResult[] Insert(ItemValueCollection[] items, bool replace, object requestHandle, UpdateCompleteEventHandler callback, out IRequest request);

        ResultCollection[] Replace(ItemValueCollection[] items);

        IdentifiedResult[] Replace(ItemValueCollection[] items, object requestHandle, UpdateCompleteEventHandler callback, out IRequest request);

        IdentifiedResult[] Delete(Time startTime, Time endTime, ItemIdentifier[] items);

        IdentifiedResult[] Delete(Time startTime, Time endTime, ItemIdentifier[] items, object requestHandle, UpdateCompleteEventHandler callback, out IRequest request);

        ResultCollection[] DeleteAtTime(ItemTimeCollection[] items);

        IdentifiedResult[] DeleteAtTime(ItemTimeCollection[] items, object requestHandle, UpdateCompleteEventHandler callback, out IRequest request);

        void CancelRequest(IRequest request);

        void CancelRequest(IRequest request, CancelCompleteEventHandler callback);
    }
}