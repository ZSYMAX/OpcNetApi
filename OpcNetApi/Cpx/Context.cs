using System;

namespace Opc.Cpx
{
    internal struct Context
    {
        public Context(byte[] buffer)
        {
            this.Buffer = buffer;
            this.Index = 0;
            this.Dictionary = null;
            this.Type = null;
            this.BigEndian = false;
            this.CharWidth = 2;
            this.StringEncoding = "UCS-2";
            this.FloatFormat = "IEEE-754";
        }

        public const string STRING_ENCODING_ACSII = "ASCII";

        public const string STRING_ENCODING_UCS2 = "UCS-2";

        public const string FLOAT_FORMAT_IEEE754 = "IEEE-754";

        public byte[] Buffer;

        public int Index;

        public TypeDictionary Dictionary;

        public TypeDescription Type;

        public bool BigEndian;

        public int CharWidth;

        public string StringEncoding;

        public string FloatFormat;
    }
}