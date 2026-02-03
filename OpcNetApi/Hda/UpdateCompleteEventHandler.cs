using System;

namespace Opc.Hda
{
    public delegate void UpdateCompleteEventHandler(IRequest request, ResultCollection[] results);
}