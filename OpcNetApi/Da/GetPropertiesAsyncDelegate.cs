using System;

namespace Opc.Da
{
    public delegate ItemPropertyCollection[] GetPropertiesAsyncDelegate(ItemIdentifier[] itemIDs, PropertyID[] propertyIDs, string itemPath, bool returnValues);
}