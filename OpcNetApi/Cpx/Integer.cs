using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Opc.Cpx
{
    [XmlInclude(typeof(Int16))]
    [XmlInclude(typeof(UInt16))]
    [XmlInclude(typeof(UInt32))]
    [XmlInclude(typeof(UInt8))]
    [XmlInclude(typeof(Int8))]
    [XmlInclude(typeof(UInt64))]
    [XmlType(Namespace = "http://opcfoundation.org/OPCBinary/1.0/")]
    [XmlInclude(typeof(Int64))]
    [XmlInclude(typeof(Int32))]
    public class Integer : FieldType
    {
        [XmlAttribute]
        [DefaultValue(true)]
        public bool Signed = true;
    }
}