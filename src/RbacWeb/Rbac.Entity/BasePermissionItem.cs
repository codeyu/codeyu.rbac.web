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
    [TableName("Base_PermissionItem")]
    [PrimaryKey("Id")]
    [Description("操作权限项")]
    [Key("Id")]
    public class BasePermissionItem
    {

        /// <summary>
        /// 操作权限项主键
        /// </summary>
        [Description("操作权限项主键")]
        public string Id { get; set; }
        /// <summary>
        /// 上级操作权限项主键
        /// </summary>
        [Description("上级操作权限项主键")]
        public string ParentId { get; set; }
        /// <summary>
        /// 操作权限项名称
        /// </summary>
        [Description("操作权限项名称")]
        public string Name { get; set; }
        /// <summary>
        /// 操作权限项编码
        /// </summary>
        [Description("操作权限项编码")]
        public string Code { get; set; }
        /// <summary>
        /// 备注说明
        /// </summary>
        [Description("备注说明")]
        public string Description { get; set; }
        /// <summary>
        /// 是否允许编辑
        /// </summary>
        [Description("是否允许编辑")]
        public int AllowUpdate { get; set; }
        /// <summary>
        /// 是否允许删除
        /// </summary>
        [Description("是否允许删除")]
        public int AllowDelete { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [Description("是否启用")]
        public int Enabled { get; set; }
        /// <summary>
        /// 排序值
        /// </summary>
        [Description("排序值")]
        public string SortCode { get; set; }
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
        /// <summary>
        /// 上级操作按钮名称
        /// </summary>
        [ResultColumn]
        public string ParentName { get; set; }
        /// <summary>
        /// 上级操作按钮排序值
        /// </summary>
        [ResultColumn]
        public string ParentSortCode { get; set; }
    }
}
