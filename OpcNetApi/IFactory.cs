using System;

namespace Opc
{
    public interface IFactory : IDisposable
    {
        IServer CreateInstance(URL url, ConnectData connectData);
    }
}