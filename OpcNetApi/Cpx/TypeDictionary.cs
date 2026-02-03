using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Opc.Cpx
{
    [XmlType(Namespace = "http://opcfoundation.org/OPCBinary/1.0/")]
    [XmlRoot(Namespace = "http://opcfoundation.org/OPCBinary/1.0/", IsNullable = false)]
    public class TypeDictionary
    {
        [XmlElement("TypeDescription")]
        public TypeDescription[] TypeDescription;

        [XmlAttribute]
        public string DictionaryName;

        [XmlAttribute]
        [DefaultValue(true)]
        public bool DefaultBigEndian = true;

        [XmlAttribute]
        [DefaultValue("UCS-2")]
        public string DefaultStringEncoding = "UCS-2";

        [XmlAttribute]
        [DefaultValue(2)]
        public int DefaultCharWidth = 2;

        [XmlAttribute]
        [DefaultValue("IEEE-754")]
        public string DefaultFloatFormat = "IEEE-754";
    }
}