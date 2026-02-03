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
                    server = ComplexTypeCache.m_server;
                }

                return server;
            }
            set
            {
                lock (typeof(ComplexTypeCache))
                {
                    ComplexTypeCache.m_server = value;
                    ComplexTypeCache.m_items.Clear();
                    ComplexTypeCache.m_dictionaries.Clear();
                    ComplexTypeCache.m_descriptions.Clear();
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
                    complexItem.Update(ComplexTypeCache.m_server);
                }
                catch
                {
                    complexItem = null;
                }

                ComplexTypeCache.m_items[itemID.Key] = complexItem;
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
                complexItem = ComplexTypeCache.GetComplexItem(new ItemIdentifier(element.ItemPath, element.ItemName));
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
                string text = (string)ComplexTypeCache.m_dictionaries[itemID.Key];
                if (text != null)
                {
                    result = text;
                }
                else
                {
                    ComplexItem complexItem = ComplexTypeCache.GetComplexItem(itemID);
                    if (complexItem != null)
                    {
                        text = complexItem.GetTypeDictionary(ComplexTypeCache.m_server);
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
                ComplexItem complexItem = ComplexTypeCache.GetComplexItem(itemID);
                if (complexItem != null)
                {
                    text = (string)ComplexTypeCache.m_descriptions[complexItem.TypeItemID.Key];
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