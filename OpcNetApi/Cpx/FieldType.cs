using System;
using System.Xml.Serialization;

namespace Opc.Cpx
{
    [XmlInclude(typeof(BitString))]
    [XmlInclude(typeof(UInt16))]
    [XmlInclude(typeof(Int64))]
    [XmlInclude(typeof(Int32))]
    [XmlInclude(typeof(Int16))]
    [XmlInclude(typeof(Int8))]
    [XmlInclude(typeof(TypeReference))]
    [XmlInclude(typeof(CharString))]
    [XmlInclude(typeof(UInt32))]
    [XmlInclude(typeof(UInt8))]
    [XmlType(Namespace = "http://opcfoundation.org/OPCBinary/1.0/")]
    [XmlInclude(typeof(FloatingPoint))]
    [XmlInclude(typeof(Unicode))]
    [XmlInclude(typeof(Ascii))]
    [XmlInclude(typeof(Single))]
    [XmlInclude(typeof(UInt64))]
    [XmlInclude(typeof(Double))]
    [XmlInclude(typeof(Integer))]
    public class FieldType
    {
        [XmlAttribute]
        public string Name;

        [XmlAttribute]
        public string Format;

        [XmlAttribute]
        public int Length;

        [XmlIgnore]
        public bool LengthSpecified;

        [XmlAttribute]
        public int ElementCount;

        [XmlIgnore]
        public bool ElementCountSpecified;

        [XmlAttribute]
        public string ElementCountRef;

        [XmlAttribute]
        public string FieldTerminator;
    }
}