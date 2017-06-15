using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.Entity
{
    /// <summary>
    /// 数据表字段实体
    /// </summary>
    public class DataBaseField
    {
        public string number { get; set; }
        public string column { get; set; }
        public string datatype { get; set; }
        public string length { get; set; }
        public string key { get; set; }
        public string isnullable { get; set; }
        public string remark { get; set; }
    }
}
