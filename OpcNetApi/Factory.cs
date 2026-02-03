using System;
using System.Runtime.Serialization;

namespace Opc
{
    [Serializable]
    public class Factory : IFactory, IDisposable, ISerializable, ICloneable
    {
        public Factory(System.Type systemType, bool useRemoting)
        {
            this.m_systemType = systemType;
            this.m_useRemoting = useRemoting;
        }

        ~Factory()
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

        protected Factory(SerializationInfo info, StreamingContext context)
        {
            this.m_useRemoting = info.GetBoolean("UseRemoting");
            this.m_systemType = (System.Type)info.GetValue("SystemType", typeof(Type));
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("UseRemoting", this.m_useRemoting);
            info.AddValue("SystemType", this.m_systemType);
        }

        public virtual object Clone()
        {
            return base.MemberwiseClone();
        }

        public virtual IServer CreateInstance(URL url, ConnectData connectData)
        {
            IServer result;
            if (!this.m_useRemoting)
            {
                result = (IServer)Activator.CreateInstance(this.m_systemType, new object[]
                {
                    url,
                    connectData
                });
            }
            else
            {
                //result = (IServer)Activator.GetObject(this.m_systemType, url.ToString());
                result = null;
            }

            return result;
        }

        protected System.Type SystemType
        {
            get { return this.m_systemType; }
            set { this.m_systemType = value; }
        }

        protected bool UseRemoting
        {
            get { return this.m_useRemoting; }
            set { this.m_useRemoting = value; }
        }

        private bool m_disposed;

        private System.Type m_systemType;

        private bool m_useRemoting;

        private class Names
        {
            internal const string USE_REMOTING = "UseRemoting";

            internal const string SYSTEM_TYPE = "SystemType";
        }
    }
}