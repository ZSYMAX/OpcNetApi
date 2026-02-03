using System;

namespace Opc.Ae
{
    public interface IServer : Opc.IServer, IDisposable
    {
        ServerStatus GetStatus();

        ISubscription CreateSubscription(SubscriptionState state);

        int QueryAvailableFilters();

        Category[] QueryEventCategories(int eventType);

        string[] QueryConditionNames(int eventCategory);

        string[] QuerySubConditionNames(string conditionName);

        string[] QueryConditionNames(string sourceName);

        Attribute[] QueryEventAttributes(int eventCategory);

        ItemUrl[] TranslateToItemIDs(string sourceName, int eventCategory, string conditionName, string subConditionName, int[] attributeIDs);

        Condition GetConditionState(string sourceName, string conditionName, int[] attributeIDs);

        ResultID[] EnableConditionByArea(string[] areas);

        ResultID[] DisableConditionByArea(string[] areas);

        ResultID[] EnableConditionBySource(string[] sources);

        ResultID[] DisableConditionBySource(string[] sources);

        EnabledStateResult[] GetEnableStateByArea(string[] areas);

        EnabledStateResult[] GetEnableStateBySource(string[] sources);

        ResultID[] AcknowledgeCondition(string acknowledgerID, string comment, EventAcknowledgement[] conditions);

        BrowseElement[] Browse(string areaID, BrowseType browseType, string browseFilter);

        BrowseElement[] Browse(string areaID, BrowseType browseType, string browseFilter, int maxElements, out IBrowsePosition position);

        BrowseElement[] BrowseNext(int maxElements, ref IBrowsePosition position);
    }
}