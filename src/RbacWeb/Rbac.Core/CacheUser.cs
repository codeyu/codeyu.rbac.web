using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.Core
{ 
    /// <summary>
    ///  存 缓存对象
    /// </summary>
    public class CacheUser
    {
        /// <summary>
        /// 用户主键
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 登录账户
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string UserPassword { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// 默认角色主键
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 默认角色名称
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 岗位主键
        /// </summary>
        public string JobTitle { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string JobTitleName { get; set; }
        /// <summary>
        /// 单位主键
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 分部主键
        /// </summary>
        public string SubCompanyId { get; set; }
        /// <summary>
        /// 分部名称
        /// </summary>
        public string SubCompanyName { get; set; }
        /// <summary>
        /// 部门主键
        /// </summary>
        public string DepartmentId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// 工作组主键
        /// </summary>
        public string WorkGroupId { get; set; }
        /// <summary>
        /// 工作组名称
        /// </summary>
        public string WorkGroupName { get; set; }
    }
}
