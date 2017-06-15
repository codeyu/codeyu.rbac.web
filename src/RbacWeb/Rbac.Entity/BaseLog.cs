using Rbac.Core;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.Entity
{
    [Serializable]
    [TableName("Base_Log")]
    [PrimaryKey("Id")]
    [Description("操作日志")]
    [Key("Id")]
    public class BaseLog
    {
        /// <summary>
        /// 日志主键
        /// </summary>
        [Description("日志主键")]
        public string Id { get; set; }
        /// <summary>
        /// 操作人主键
        /// </summary>
        [Description("操作人主键")]
        public string UserId { get; set; }
        /// <summary>
        /// 操作账号
        /// </summary>
        [Description("操作账号")]
        public string UserName { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        [Description("操作人")]
        public string RealName { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        [Description("位置")]
        public string NavigationUrl { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        [Description("IP地址")]
        public string IPAddress { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        [Description("操作类型")]
        public int Type { get; set; }
        /// <summary>
        /// 操作日期
        /// </summary>
        [Description("操作日期")]
        public string Date { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [Description("描述")]
        public string Description { get; set; }
        /// <summary>
        /// 旧值
        /// </summary>
        [Description("旧值")]
        public string OldXml { get; set; }
        /// <summary>
        /// 新值
        /// </summary>
        [Description("新值")]
        public string NewXml { get; set; }
        /// <summary>
        /// 操作类型名称
        /// </summary>
        [ResultColumn]
        public string TypeName { get; set; }
    }
}
