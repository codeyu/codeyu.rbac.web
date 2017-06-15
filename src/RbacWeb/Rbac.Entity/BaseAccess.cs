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
    [TableName("Base_Access")]
    [PrimaryKey("Id")]
    [Description("操作日志")]
    [Key("Id")]
    public class BaseAccess
    {  
        /// <summary>
        /// 访问日志主键
        /// </summary>
        [Description("访问日志主键")]
        public string Id { get; set; }

        /// <summary>
        /// 用户主键
        /// </summary>
        [Description("用户主键")]
        public string UserId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [Description("用户名称")]
        public string UserName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Description("姓名")]
        public string RealName { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        [Description("IP地址")]
        public string IPAddress { get; set; }

        /// <summary>
        /// 机器名
        /// </summary>
        [Description("机器名")]
        public string MachineName { get; set; }

        /// <summary>
        /// 登陆日期
        /// </summary>
        [Description("登陆日期")]
        public string Date { get; set; }

        /// <summary>
        /// 登陆状态
        /// </summary>
        [Description("登陆状态")]
        public int Enabled { get; set; }

        /// <summary>
        /// 备注说明
        /// </summary>
        [Description("备注说明")]
        public string Description { get; set; }

        /// <summary>
        /// 登陆日期
        /// </summary>
        [ResultColumn]
        public int day { get; set; }

        /// <summary>
        /// 登陆次数
        /// </summary>
        [ResultColumn]
        public int count { get; set; }
      
    }
}
