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
    [TableName("Base_Module")]
    [PrimaryKey("Id")]
    [Description("功能模块")]
    [Key("Id")]
    public class BaseModule
    {
        /// <summary>
        /// 模块主键
        /// </summary>
        [Description("模块主键")]
        public string Id { get; set; }
        /// <summary>
        /// 上级模块主键
        /// </summary>
        [Description("上级模块主键")]
        public string ParentId { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        [Description("模块名称")]
        public string Name { get; set; }
        /// <summary>
        /// 模块编码
        /// </summary>
        [Description("模块编码")]
        public string Code { get; set; }
        /// <summary>
        /// 图标链接
        /// </summary>
        [Description("图标链接")]
        public string IconUrl { get; set; }
        /// <summary>
        /// 备注说明
        /// </summary>
        [Description("备注说明")]
        public string Description { get; set; }
        /// <summary>
        /// 导航地址
        /// </summary>
        [Description("导航地址")]
        public string NavigationUrl { get; set; }
        /// <summary>
        /// 窗口目标
        /// </summary>
        [Description("窗口目标")]
        public string NavigationTarget { get; set; }
        /// <summary>
        /// 是否可见
        /// </summary>
        [Description("是否可见")]
        public int IsVisible { get; set; }
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
        /// 上级模块名称
        /// </summary>
        [ResultColumn]
        public string ParentName { get; set; }
        /// <summary>
        /// 上级排序值
        /// </summary>
        [ResultColumn]
        public string ParentSortCode { get; set; }
    }
}
