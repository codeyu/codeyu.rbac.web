using Murphy.Core;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murphy.Entity
{
    [Serializable]
    [TableName("Base_Parameter")]
    [PrimaryKey("Id")]
    [Description("系统参数")]
    [Key("Id")]
    public class BaseParameter
    {
        /// <summary>
        /// 参数主键
        /// </summary>
        [Description("参数主键")]
        public string Id { get; set; }
        /// <summary>
        /// 参数编码
        /// </summary>
        [Description("参数编码")]
        public string Code { get; set; }
        /// <summary>
        /// 参数值
        /// </summary>
        [Description("参数值")]
        public string Value { get; set; }
        /// <summary>
        /// 备注说明
        /// </summary>
        [Description("备注说明")]
        public string Description { get; set; }
        /// <summary>
        /// 排序值
        /// </summary>
        [Description("排序值")]
        public int SortOrder { get; set; }
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
