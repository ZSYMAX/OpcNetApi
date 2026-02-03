using System;
using System.Xml.Serialization;

namespace Opc.Cpx
{
    [XmlInclude(typeof(Unicode))]
    [XmlType(Namespace = "http://opcfoundation.org/OPCBinary/1.0/")]
    [XmlInclude(typeof(Ascii))]
    public class CharString : FieldType
    {
        [XmlAttribute]
        public int CharWidth;

        [XmlIgnore]
        public bool CharWidthSpecified;

        [XmlAttribute]
        public string StringEncoding;

        [XmlAttribute]
        public string CharCountRef;
    }
}