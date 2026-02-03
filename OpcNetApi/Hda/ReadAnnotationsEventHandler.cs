using System;

namespace Opc.Hda
{
    public delegate void ReadAnnotationsEventHandler(IRequest request, AnnotationValueCollection[] results);
}