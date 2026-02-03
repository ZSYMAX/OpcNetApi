using System;

namespace Opc
{
    public interface IRequest
    {
        object Handle { get; }
    }
}