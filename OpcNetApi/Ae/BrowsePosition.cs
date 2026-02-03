using System;

namespace Opc.Ae
{
    [Serializable]
    public class BrowsePosition : IBrowsePosition, IDisposable, ICloneable
    {
        public BrowsePosition(string areaID, BrowseType browseType, string browseFilter)
        {
            m_areaID = areaID;
            m_browseType = browseType;
            m_browseFilter = browseFilter;
        }

        public string AreaID => m_areaID;

        public BrowseType BrowseType => m_browseType;

        public string BrowseFilter => m_browseFilter;

        ~BrowsePosition()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                m_disposed = true;
            }
        }

        public virtual object Clone()
        {
            return (BrowsePosition)MemberwiseClone();
        }

        private bool m_disposed;

        private string m_areaID;

        private BrowseType m_browseType;

        private string m_browseFilter;
    }
}