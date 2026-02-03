using System;
using System.Collections;
using Opc.Da;

namespace Opc.Cpx
{
    public class ComplexTypeCache
    {
        public static Da.Server Server
        {
            get
            {
                Da.Server server;
                lock (typeof(ComplexTypeCache))
                {
                    server = m_server;
                }

                return server;
            }
            set
            {
                lock (typeof(ComplexTypeCache))
                {
                    m_server = value;
                    m_items.Clear();
                    m_dictionaries.Clear();
                    m_descriptions.Clear();
                }
            }
        }

        public static ComplexItem GetComplexItem(ItemIdentifier itemID)
        {
            if (itemID == null)
            {
                return null;
            }

            ComplexItem result;
            lock (typeof(ComplexTypeCache))
            {
                ComplexItem complexItem = new ComplexItem(itemID);
                try
                {
                    complexItem.Update(m_server);
                }
                catch
                {
                    complexItem = null;
                }

                m_items[itemID.Key] = complexItem;
                result = complexItem;
            }

            return result;
        }

        public static ComplexItem GetComplexItem(BrowseElement element)
        {
            if (element == null)
            {
                return null;
            }

            ComplexItem complexItem;
            lock (typeof(ComplexTypeCache))
            {
                complexItem = GetComplexItem(new ItemIdentifier(element.ItemPath, element.ItemName));
            }

            return complexItem;
        }

        public static string GetTypeDictionary(ItemIdentifier itemID)
        {
            if (itemID == null)
            {
                return null;
            }

            string result;
            lock (typeof(ComplexTypeCache))
            {
                string text = (string)m_dictionaries[itemID.Key];
                if (text != null)
                {
                    result = text;
                }
                else
                {
                    ComplexItem complexItem = GetComplexItem(itemID);
                    if (complexItem != null)
                    {
                        text = complexItem.GetTypeDictionary(m_server);
                    }

                    result = text;
                }
            }

            return result;
        }

        public static string GetTypeDescription(ItemIdentifier itemID)
        {
            if (itemID == null)
            {
                return null;
            }

            string result;
            lock (typeof(ComplexTypeCache))
            {
                string text = null;
                ComplexItem complexItem = GetComplexItem(itemID);
                if (complexItem != null)
                {
                    text = (string)m_descriptions[complexItem.TypeItemID.Key];
                    if (text != null)
                    {
                        return text;
                    }

                    text = ((string)(m_descriptions[complexItem.TypeItemID.Key] = complexItem.GetTypeDescription(m_server)));
                }

                result = text;
            }

            return result;
        }

        private static Da.Server m_server = null;

        private static Hashtable m_items = new Hashtable();

        private static Hashtable m_dictionaries = new Hashtable();

        private static Hashtable m_descriptions = new Hashtable();
    }
}