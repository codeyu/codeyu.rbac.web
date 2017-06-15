using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rbac.Utils
{
    public class HashtableHelper
    {
        /// <summary>
        /// 字符串 分割转换 Hashtable   ≌; ☻
        /// </summary>
        public static Hashtable StringKeyValueToHashtable(string str)
        {
            Hashtable ht = new Hashtable();
            if (!string.IsNullOrEmpty(str))
            {
                string[] kevyValueParamArray = str.Split('≌');
                foreach (string item in kevyValueParamArray)
                {
                    if (item.Length > 0)
                    {
                        string[] keyValue = item.Split('☻');
                        ht[keyValue[0]] = keyValue[1];
                    }
                }
            }
            return ht;
        }
    }
}
