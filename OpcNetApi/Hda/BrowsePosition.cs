using System;

namespace Opc.Hda
{
    [Serializable]
    public class BrowsePosition : IBrowsePosition, IDisposable, ICloneable
    {
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
    }
}