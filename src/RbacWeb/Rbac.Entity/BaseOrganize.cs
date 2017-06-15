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
    [TableName("Base_Organize")]
    [PrimaryKey("Id")]
    [Description("组织机构")]
    [Key("Id")]
    public class BaseOrganize
    {
        /// <summary>
        /// 机构主键
        /// </summary>
        [Description("机构主键")]
        public string Id { get; set; }
        /// <summary>
        /// 上级机构主键
        /// </summary>
        [Description("上级机构主键")]
        public string ParentId { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        [Description("机构名称")]
        public string Name { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        [Description("机构编码")]
        public string Code { get; set; }
        /// <summary>
        /// 机构描述
        /// </summary>
        [Description("机构描述")]
        public string Description { get; set; }
        /// <summary>
        /// 机构类型
        /// </summary>
        [Description("机构类型")]
        public int Type { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [Description("是否启用")]
        public int Enabled { get; set; }
        /// <summary>
        /// 排序值
        /// </summary>
        [Description("排序值")]
        public int SortCode { get; set; }
        /// <summary>
        /// 机构全称
        /// </summary>
        [Description("机构全称")]
        public string FullName { get; set; }
        /// <summary>
        /// 办公地点
        /// </summary>
        [Description("办公地点")]
        public string Office { get; set; }
        /// <summary>
        /// 管理员手机号码
        /// </summary>
        [Description("管理员手机号码")]
        public string MobilePhone { get; set; }
        /// <summary>
        /// 创建人主键
        /// </summary>
        [Description("创建人主键")]
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [Description("创建日期")]
        public string CreateDate { get; set; }
        /// <summary>
        /// 修改人主键
        /// </summary>
        [Description("修改人主键")]
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Description("修改时间")]
        public string ModifyDate { get; set; }
        /// <summary>
        /// 上级机构名称
        /// </summary>
        [ResultColumn]
        public string ParentName { get; set; }
        /// <summary>
        /// 节点选中
        /// </summary>
        [ResultColumn]
        public bool isChecked { get; set; }
        /// <summary>
        /// 子节点个数
        /// </summary>
         [ResultColumn]
        public string Amount { get; set; }
        /// <summary>
        /// 是否是叶子节点
        /// </summary>
         [ResultColumn]
        public bool isleaf { get; set; }
    }
}
