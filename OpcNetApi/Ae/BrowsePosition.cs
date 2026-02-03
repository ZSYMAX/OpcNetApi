using System;

namespace Opc.Ae
{
    [Serializable]
    public class BrowsePosition : IBrowsePosition, IDisposable, ICloneable
    {
        public BrowsePosition(string areaID, BrowseType browseType, string browseFilter)
        {
            this.m_areaID = areaID;
            this.m_browseType = browseType;
            this.m_browseFilter = browseFilter;
        }

        public string AreaID
        {
            get { return this.m_areaID; }
        }

        public BrowseType BrowseType
        {
            get { return this.m_browseType; }
        }

        public string BrowseFilter
        {
            get { return this.m_browseFilter; }
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

        private string m_areaID;

        private BrowseType m_browseType;

        private string m_browseFilter;
    }
}