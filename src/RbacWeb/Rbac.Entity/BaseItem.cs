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
    [TableName("Base_Item")]
    [PrimaryKey("Id")]
    [Description("字典类别")]
    [Key("Id")]
    public class BaseItem
    {
        /// <summary>
        /// 字典类别主键
        /// </summary>
        [Description("字典类别主键")]
        public string Id { get; set; }
        /// <summary>
        /// 父级主键
        /// </summary>
        [Description("父级主键")]
        public string ParentId { get; set; }
        /// <summary>
        /// 字典类别编码
        /// </summary>
        [Description("字典类别编码")]
        public string Code { get; set; }
        /// <summary>
        /// 字典类别值
        /// </summary>
        [Description("字典类别值")]
        public string Name { get; set; }
        /// <summary>
        /// 字典类别描述
        /// </summary>
        [Description("字典类别描述")]
        public string Description { get; set; }
        /// <summary>
        /// 字典类别状态
        /// </summary>
        [Description("字典类别状态")]
        public int Enabled { get; set; }
        /// <summary>
        /// 排序值
        /// </summary>
        [Description("排序值")]
        public int SortCode { get; set; }
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
    }
}
