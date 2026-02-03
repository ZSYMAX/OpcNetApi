using System;

namespace Opc
{
    public interface IResult
    {
        ResultID ResultID { get; set; }

        string DiagnosticInfo { get; set; }
    }
}