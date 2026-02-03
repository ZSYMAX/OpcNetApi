using System;
using System.Xml.Serialization;

namespace Opc.Cpx
{
    [XmlType(Namespace = "http://opcfoundation.org/OPCBinary/1.0/")]
    public class TypeDescription
    {
        [XmlElement("Field")]
        public FieldType[] Field;

        [XmlAttribute]
        public string TypeID;

        [XmlAttribute]
        public bool DefaultBigEndian;

        [XmlIgnore]
        public bool DefaultBigEndianSpecified;

        [XmlAttribute]
        public string DefaultStringEncoding;

        [XmlAttribute]
        public int DefaultCharWidth;

        [XmlIgnore]
        public bool DefaultCharWidthSpecified;

        [XmlAttribute]
        public string DefaultFloatFormat;
    }
}