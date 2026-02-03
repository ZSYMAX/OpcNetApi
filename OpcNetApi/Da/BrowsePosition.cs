using System;

namespace Opc.Da
{
    [Serializable]
    public class BrowsePosition : IBrowsePosition, IDisposable, ICloneable
    {
        public ItemIdentifier ItemID
        {
            get { return this.m_itemID; }
        }

        public BrowseFilters Filters
        {
            get { return (BrowseFilters)this.m_filters.Clone(); }
        }

        public int MaxElementsReturned
        {
            get { return this.m_filters.MaxElementsReturned; }
            set { this.m_filters.MaxElementsReturned = value; }
        }

        public BrowsePosition(ItemIdentifier itemID, BrowseFilters filters)
        {
            if (filters == null)
            {
                throw new ArgumentNullException("filters");
            }

            this.m_itemID = ((itemID != null) ? ((ItemIdentifier)itemID.Clone()) : null);
            this.m_filters = (BrowseFilters)filters.Clone();
        }

        ~BrowsePosition()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.m_disposed)
            {
                this.m_disposed = true;
            }
        }

        public virtual object Clone()
        {
            return (BrowsePosition)base.MemberwiseClone();
        }

        private bool m_disposed;

        private BrowseFilters m_filters;

        private ItemIdentifier m_itemID;
    }
}