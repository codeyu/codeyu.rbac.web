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
    [TableName("Base_JobTitle")]
    [PrimaryKey("Id")]
    [Description("岗位管理")]
    [Key("Id")]
    public class BaseJobTitle
    {
        /// <summary>
        /// 岗位主键
        /// </summary>
        [Description("岗位主键")]
        public string Id { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        [Description("岗位名称")]
        public string Name { get; set; }
        /// <summary>
        /// 岗位编码
        /// </summary>
        [Description("岗位编码")]
        public string Code { get; set; }
        /// <summary>
        /// 岗位说明
        /// </summary>
        [Description("岗位说明")]
        public string Description { get; set; }
        /// <summary>
        /// 部门主键
        /// </summary>
        [Description("部门主键")]
        public string DepartmentId { get; set; }
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
