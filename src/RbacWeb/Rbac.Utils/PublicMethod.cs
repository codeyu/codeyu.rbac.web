using Murphy.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Murphy.Utils
{
    public class PublicMethod
    {
        #region 反射实体类相关方法
        /// <summary>
        /// 获取实体类主键字段
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static object GetKeyField(Type t)
        {
            string _KeyField = "";
            KeyAttribute KeyField;
            var name = t.Name;
            foreach (Attribute attr in t.GetCustomAttributes(true))
            {
                KeyField = attr as KeyAttribute;
                if (KeyField != null)
                    _KeyField = KeyField.Name;
            }
            return _KeyField;
        }

        /// <summary>
        /// 获取实体类主键字段值
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public static object GetKeyFieldValue<T>(T entity)
        {
            Type objTye = typeof(T);
            string _KeyField = "";
            KeyAttribute KeyField;
            var name = objTye.Name;
            foreach (Attribute attr in objTye.GetCustomAttributes(true))
            {
                KeyField = attr as KeyAttribute;
                if (KeyField != null)
                    _KeyField = KeyField.Name;
            }
            PropertyInfo property = objTye.GetProperty(_KeyField);
            return property.GetValue(entity, null).ToString();
        }

        /// <summary>
        /// 获取实体类中文名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetBusingessName<T>()
        {
            Type objTye = typeof(T);
            string entityName = "";
            var busingessNames = objTye.GetCustomAttributes(true).OfType<DescriptionAttribute>();
            var descriptionAttributes = busingessNames as DescriptionAttribute[] ?? busingessNames.ToArray();
            if (descriptionAttributes.Any())
                entityName = descriptionAttributes.ToList()[0].Description;
            else
            {
                entityName = objTye.Name;
            }
            return entityName;
        }

        /// <summary>
        /// 获取实体类各字段中文名称
        /// </summary>
        /// <param name="pi">字段属性信息</param>
        /// <returns></returns>
        public static string GetFieldText(PropertyInfo pi)
        {
            DescriptionAttribute descAttr;
            string txt = "";
            var descAttrs = pi.GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (descAttrs.Any())
            {
                descAttr = descAttrs[0] as DescriptionAttribute;
                txt = descAttr.Description;
            }
            else
            {
                txt = pi.Name;
            }
            return txt;
        }

        #endregion

        #region 游览器相关方法

        /// <summary>
        /// 得到用户浏览器类型
        /// </summary>
        /// <returns></returns>
        public static string GetBrowse()
        {
            return System.Web.HttpContext.Current.Request.Browser.Type;
        }

        /// <summary>
        /// 获取浏览器端操作系统名称
        /// </summary>
        /// <returns></returns>
        public static string GetOSName()
        {
            string osVersion = System.Web.HttpContext.Current.Request.Browser.Platform;
            string userAgent = System.Web.HttpContext.Current.Request.UserAgent;

            if (userAgent.Contains("NT 6.2"))
            {
                osVersion = "Windows8/Server 2012";
            }
            if (userAgent.Contains("NT 6.1"))
            {
                osVersion = "Windows7/Server 2008 R2";
            }
            else if (userAgent.Contains("NT 6.0"))
            {
                osVersion = "Windows Vista/Server 2008";
            }
            else if (userAgent.Contains("NT 5.2"))
            {
                osVersion = "Windows Server 2003";
            }
            else if (userAgent.Contains("NT 5.1"))
            {
                osVersion = "WindowsXP";
            }
            else if (userAgent.Contains("NT 5"))
            {
                osVersion = "Windows2000";
            }
            else if (userAgent.Contains("NT 4"))
            {
                osVersion = "WindowsNT4.0";
            }
            else if (userAgent.Contains("Me"))
            {
                osVersion = "WindowsMe";
            }
            else if (userAgent.Contains("98"))
            {
                osVersion = "Windows98";
            }
            else if (userAgent.Contains("95"))
            {
                osVersion = "Windows95";
            }
            else if (userAgent.Contains("Mac"))
            {
                osVersion = "Mac";
            }
            else if (userAgent.Contains("Unix"))
            {
                osVersion = "UNIX";
            }
            else if (userAgent.Contains("Linux"))
            {
                osVersion = "Linux";
            }
            else if (userAgent.Contains("SunOS"))
            {
                osVersion = "SunOS";
            }
            return osVersion;
        }


        #endregion

        /// <summary>
        /// 得到每页显示条数
        /// </summary>
        /// <returns></returns>
        public static int GetPageSize()
        {
            string size = System.Web.HttpContext.Current.Request.QueryString["pageSize"] ?? "10";
            int size1;
            return size.IsInt(out size1) ? size1 : 10;
        }

        /// <summary>
        /// 得到页码
        /// </summary>
        /// <returns></returns>
        public static int GetPageNumber()
        {
            string number = System.Web.HttpContext.Current.Request.QueryString["pageNumber"] ?? "1";
            int number1;
            return number.IsInt(out number1) ? number1 : 1;
        }

        /// <summary>
        /// 得到应用程序路径
        /// </summary>
        public static string GetAppPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        /// <summary>
        /// 检查目录是否存在，不存在就创建
        /// </summary>
        /// <param name="path"></param>
        public static string ExistsDirectory(string path)
        {
            string dir = System.IO.Path.GetDirectoryName(path);
            if (!System.IO.Directory.Exists(dir))
            {
                System.IO.Directory.CreateDirectory(dir);
            }
            return path;
        }
    }
}
