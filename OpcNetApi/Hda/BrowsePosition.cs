using System;

namespace Opc.Hda
{
    [Serializable]
    public class BrowsePosition : IBrowsePosition, IDisposable, ICloneable
    {
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
    }
}