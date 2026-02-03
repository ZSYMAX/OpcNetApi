using System;

namespace Opc.Cpx
{
    public class BinaryStream
    {
        protected BinaryStream()
        {
        }

        internal bool IsArrayField(FieldType field)
        {
            if (field.ElementCountSpecified)
            {
                if (field.ElementCountRef != null || field.FieldTerminator != null)
                {
                    throw new InvalidSchemaException("Multiple array size attributes specified for field '" + field.Name + " '.");
                }

                return true;
            }
            else
            {
                if (field.ElementCountRef == null)
                {
                    return field.FieldTerminator != null;
                }

                if (field.FieldTerminator != null)
                {
                    throw new InvalidSchemaException("Multiple array size attributes specified for field '" + field.Name + " '.");
                }

                return true;
            }
        }

        internal byte[] GetTerminator(Context context, FieldType field)
        {
            if (field.FieldTerminator == null)
            {
                throw new InvalidSchemaException(field.Name + " is not a terminated group.");
            }

            string text = Convert.ToString(field.FieldTerminator).ToUpper();
            byte[] array = new byte[text.Length / 2];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = System.Convert.ToByte(text.Substring(i * 2, 2), 16);
            }

            return array;
        }

        internal Context InitializeContext(byte[] buffer, TypeDictionary dictionary, string typeName)
        {
            Context result = new Context(buffer);
            result.Dictionary = dictionary;
            result.Type = null;
            result.BigEndian = dictionary.DefaultBigEndian;
            result.CharWidth = dictionary.DefaultCharWidth;
            result.StringEncoding = dictionary.DefaultStringEncoding;
            result.FloatFormat = dictionary.DefaultFloatFormat;
            TypeDescription[] typeDescription = dictionary.TypeDescription;
            int i = 0;
            while (i < typeDescription.Length)
            {
                TypeDescription typeDescription2 = typeDescription[i];
                if (typeDescription2.TypeID == typeName)
                {
                    result.Type = typeDescription2;
                    if (typeDescription2.DefaultBigEndianSpecified)
                    {
                        result.BigEndian = typeDescription2.DefaultBigEndian;
                    }

                    if (typeDescription2.DefaultCharWidthSpecified)
                    {
                        result.CharWidth = typeDescription2.DefaultCharWidth;
                    }

                    if (typeDescription2.DefaultStringEncoding != null)
                    {
                        result.StringEncoding = typeDescription2.DefaultStringEncoding;
                    }

                    if (typeDescription2.DefaultFloatFormat != null)
                    {
                        result.FloatFormat = typeDescription2.DefaultFloatFormat;
                        break;
                    }

                    break;
                }
                else
                {
                    i++;
                }
            }

            if (result.Type == null)
            {
                throw new InvalidSchemaException("Type '" + typeName + "' not found in dictionary.");
            }

            return result;
        }

        internal void SwapBytes(byte[] bytes, int index, int length)
        {
            for (int i = 0; i < length / 2; i++)
            {
                byte b = bytes[index + length - 1 - i];
                bytes[index + length - 1 - i] = bytes[index + i];
                bytes[index + i] = b;
            }
        }
    }
}