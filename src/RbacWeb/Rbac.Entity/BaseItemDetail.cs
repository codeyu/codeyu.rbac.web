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
    [TableName("Base_ItemDetail")]
    [PrimaryKey("Id")]
    [Description("字典明细")]
    [Key("Id")]
    public class BaseItemDetail
    {
        /// <summary>
        /// 字典明细主键
        /// </summary>
        [Description("字典明细主键")]
        public string Id { get; set; }
        /// <summary>
        /// 字典主键
        /// </summary>
        [Description("字典主键")]
        public string ItemId { get; set; }
        /// <summary>
        /// 字典明细值
        /// </summary>
        [Description("字典明细值")]
        public string SelectValue { get; set; }
        /// <summary>
        /// 字典明细显示文本
        /// </summary>
        [Description("字典明细显示文本")]
        public string SelectText { get; set; }
        /// <summary>
        /// 字典明细描述
        /// </summary>
        [Description("字典明细描述")]
        public string Description { get; set; }
        /// <summary>
        /// 字典明细状态
        /// </summary>
        [Description("字典明细状态")]
        public int Enabled { get; set; }
        /// <summary>
        /// 排序值
        /// </summary>
        [Description("排序值")]
        public int SortCode { get; set; }
        /// <summary>
        /// 是否是默认值
        /// </summary>
        [Description("是否是默认值")]
        public int IsDefault { get; set; }
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
        /// 所属类别名称
        /// </summary>
        [ResultColumn]
        public string ItemName{ get; set; }
    }
}
