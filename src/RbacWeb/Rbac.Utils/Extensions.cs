using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Rbac.Utils
{
    /// <summary>
    /// 扩展属性
    /// </summary>
    public static class MyExtensions
    {
        public static bool IsInt(this string str)
        {
            int test;
            return int.TryParse(str, out test);
        }
        public static bool IsInt(this string str, out int test)
        {
            return int.TryParse(str, out test);
        }
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// 序列化对象为Xml字符串
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <returns>Xml格式字符串</returns>
        public static string SerializeXml(this object obj)
        {
            if (obj == null) { return ""; }
            Type type = obj.GetType();
            if (type.IsSerializable)
            {
                try
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(type);
                    XmlWriterSettings xset = new XmlWriterSettings();
                    xset.CloseOutput = true;
                    xset.Encoding = Encoding.UTF8;
                    xset.Indent = true;
                    xset.CheckCharacters = false;
                    System.Xml.XmlWriter xw = System.Xml.XmlWriter.Create(sb, xset);
                    xs.Serialize(xw, obj);
                    xw.Flush();
                    xw.Close();
                    return sb.ToString();
                }
                catch { return ""; }
            }
            else
            {
                return "";
            }
        }
    }
}
