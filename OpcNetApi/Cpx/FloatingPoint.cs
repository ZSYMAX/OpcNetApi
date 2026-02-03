using System;
using System.Xml.Serialization;

namespace Opc.Cpx
{
    [XmlInclude(typeof(Single))]
    [XmlType(Namespace = "http://opcfoundation.org/OPCBinary/1.0/")]
    [XmlInclude(typeof(Double))]
    public class FloatingPoint : FieldType
    {
        [XmlAttribute]
        public string FloatFormat;
    }
}