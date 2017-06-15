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
    [TableName("Base_User")]
    [PrimaryKey("Id")]
    [Description("用户管理")]
    [Key("Id")]
    public class BaseUser
    {
        /// <summary>
        /// 用户主键
        /// </summary>
        [Description("用户主键")]
        public string Id { get; set; }
        /// <summary>
        /// 登陆账号
        /// </summary>
        [Description("登陆账号")]
        public string UserName { get; set; }
        /// <summary>
        /// 用户编码
        /// </summary>
        [Description("用户编码")]
        public string Code { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [Description("姓名")]
        public string RealName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Description("密码")]
        public string UserPassword { get; set; }
        /// <summary>
        /// 单位主键
        /// </summary>
        [Description("单位主键")]
        public string CompanyId { get; set; }
        /// <summary>
        /// 分部主键
        /// </summary>
        [Description("分部主键")]
        public string SubCompanyId { get; set; }
        /// <summary>
        /// 部门主键
        /// </summary>
        [Description("部门主键")]
        public string DepartmentId { get; set; }
        /// <summary>
        /// 工作组主键
        /// </summary>
        [Description("工作组主键")]
        public string WorkGroupId { get; set; }
        /// <summary>
        /// 直接上级
        /// </summary>
        [Description("直接上级")]
        public string ManagerId { get; set; }
        /// <summary>
        /// 角色主键
        /// </summary>
        [Description("角色主键")]
        public string RoleId { get; set; }
        /// <summary>
        /// 岗位主键
        /// </summary>
        [Description("岗位主键")]
        public string JobTitle { get; set; }
        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        [Description("是否是超级管理员")]
        public int IsSuper { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Description("性别")]
        public int Gender { get; set; }
        /// <summary>
        /// 移动电话
        /// </summary>
        [Description("移动电话")]
        public string MobilePhone { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        [Description("电子邮箱")]
        public string Email { get; set; }
        /// <summary>
        /// 登陆次数
        /// </summary>
        [Description("登陆次数")]
        public int LoginTimes { get; set; }
        /// <summary>
        /// 最后登陆时间
        /// </summary>
        [Description("最后登陆时间")]
        public string LastLoginDate { get; set; }
        /// <summary>
        /// 安全级别
        /// </summary>
        [Description("安全级别")]
        public int SecLevel { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [Description("是否启用")]
        public int Enabled { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [Description("描述")]
        public string Description { get; set; }
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
        /// 角色名称
        /// </summary>
        [ResultColumn]
        public string RoleName { get; set; }
        /// <summary>
        /// 状态显示名
        /// </summary>
        [ResultColumn]
        public string EnabledName { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        [ResultColumn]
        public string JobTitleName { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        [ResultColumn]
        public string CompanyName { get; set; }
        /// <summary>
        /// 分部名称
        /// </summary>
        [ResultColumn]
        public string SubCompanyName { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        [ResultColumn]
        public string DepartmentName { get; set; }
        /// <summary>
        /// 工作组名称
        /// </summary>
        [ResultColumn]
        public string WorkGroupName { get; set; }
        /// <summary>
        /// 直接上级
        /// </summary>
        [ResultColumn]
        public string ManagerName { get; set; }
    }
}
