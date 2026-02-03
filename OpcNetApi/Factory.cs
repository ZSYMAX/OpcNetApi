using System;
using System.Runtime.Serialization;

namespace Opc
{
    [Serializable]
    public class Factory : IFactory, IDisposable, ISerializable, ICloneable
    {
        public Factory(System.Type systemType, bool useRemoting)
        {
            m_systemType = systemType;
            m_useRemoting = useRemoting;
        }

        ~Factory()
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

        protected Factory(SerializationInfo info, StreamingContext context)
        {
            m_useRemoting = info.GetBoolean("UseRemoting");
            m_systemType = (System.Type)info.GetValue("SystemType", typeof(Type));
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("UseRemoting", m_useRemoting);
            info.AddValue("SystemType", m_systemType);
        }

        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        public virtual IServer CreateInstance(URL url, ConnectData connectData)
        {
            IServer result;
            if (!m_useRemoting)
            {
                result = (IServer)Activator.CreateInstance(m_systemType, new object[]
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
            get => m_systemType;
            set => m_systemType = value;
        }

        protected bool UseRemoting
        {
            get => m_useRemoting;
            set => m_useRemoting = value;
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