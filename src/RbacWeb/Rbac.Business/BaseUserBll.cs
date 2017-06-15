using Rbac.Core;
using Rbac.Entity;
using Rbac.IData;
using Rbac.Utils;
using Newtonsoft.Json;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Rbac.Business
{
    public class BaseUserBll : BaseBll<BaseUser>
    {
        protected IBaseUserDal baseUserDal = null;
        BaseAccessBll baseAccessBll = new BaseAccessBll();
        BaseParameterBll baseParameterBll = new BaseParameterBll();
        BaseStorageBll baseStorageBll = new BaseStorageBll();
        AjaxMsg msg = new AjaxMsg();

        public BaseUserBll()
        {
            IApplicationContext springContext = ContextRegistry.GetContext();
            baseUserDal = springContext.GetObject("BaseUserDal") as IBaseUserDal;
            base.idal = baseUserDal;
        }

      

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPassword"></param>
        /// <param name="isAlways"></param>
        /// <returns></returns>
        public AjaxMsg LoginUser(string userName, string userPassword, string isAlways)
        {
            string ipAddress = RequestHelper.GetIPAddress();
            BaseUser baseUser = baseUserDal.LoginUser(userName, userPassword);
            
       
            BaseAccess baseAccess = new BaseAccess();
            baseAccess.Id = Guid.NewGuid().ToString();
            baseAccess.UserName = userName;
            baseAccess.IPAddress = ipAddress;
            baseAccess.MachineName = string.Empty;
            baseAccess.Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //获取系统参数信息
            //是否开启登陆日志
            string openAccessLog = baseParameterBll.GetListWhere(" Code='OpenAccessLog'", null).SingleOrDefault().Value;
            if (baseUser != null)
            {
                //是否开启操作日志
                string openLog = baseParameterBll.GetListWhere(" Code='OpenLog'", null).SingleOrDefault().Value;
                //是否开启缓存
                string openCache = baseParameterBll.GetListWhere(" Code='OpenCache'", null).SingleOrDefault().Value;
                //缓存时长
                string cacheMinute = baseParameterBll.GetListWhere(" Code='CacheMinute'", null).SingleOrDefault().Value;
                //徽标地址
                string softwareLogo = baseParameterBll.GetListWhere(" Code='SoftwareLogo'", null).SingleOrDefault().Value;
                //版本类型
                string softwareEdition = baseParameterBll.GetListWhere(" Code='SoftwareEdition'", null).SingleOrDefault().Value;
                //平台版权信息
                string softwareCopyright = baseParameterBll.GetListWhere(" Code='SoftwareCopyright'", null).SingleOrDefault().Value;
                string UserInfoKey = baseUser.Id + "_UserInfo";
                string ModulePermissionKey = baseUser.Id + "_ModulePermission";
                string ButtonPermissionKey = baseUser.Id + "_ButtonPermission";
                string FieldPermissionKey = baseUser.Id + "_FieldPermission";
                string DataPermissionKey = baseUser.Id + "_DataPermission";
                
                baseAccess.UserId = baseUser.Id;
                baseAccess.RealName = baseUser.RealName;
                if (baseUser.Enabled == 1)
                {
                    if (HashEncrypt.MD5System(userPassword.Trim()) == baseUser.UserPassword)
                    {
                        //如果选择了复选框，则要使用cookie保存数据
                        if (isAlways == "1")
                        {
                            //将用户id加密成字符串
                            string strCookieValue = HashEncrypt.EncryptUserInfo(baseUser.Id);
                            //创建cookie
                            HttpCookie cookie = new HttpCookie(UserInfoKey, strCookieValue);
                            cookie.Expires = DateTime.Now.AddDays(14);
                            cookie.Path = "/";
                            HttpContext.Current.Response.Cookies.Add(cookie);
                        }

                        //将系统参数信息存入到缓存中
                        CacheSystem cacheSystem = new CacheSystem();
                        cacheSystem.OpenLog = openLog;
                        cacheSystem.OpenAccessLog = openAccessLog;
                        cacheSystem.OpenCache = openCache;
                        cacheSystem.CacheMinute = cacheMinute;
                        cacheSystem.SoftwareLogo = softwareLogo;
                        cacheSystem.SoftwareEdition = softwareEdition;
                        cacheSystem.SoftwareCopyright = softwareCopyright;
                        RequestCache.AddCacheSystem(cacheSystem);

                        //添加用户信息
                        CacheUser cacheUser = new CacheUser();
                        cacheUser.UserId = baseUser.Id;
                        cacheUser.Code = baseUser.Code;
                        cacheUser.UserName = baseUser.UserName;
                        cacheUser.UserPassword = baseUser.UserPassword;
                        cacheUser.RealName = baseUser.RealName;
                        cacheUser.Gender = baseUser.Gender;
                        cacheUser.RoleId = baseUser.RoleId;
                        cacheUser.JobTitle = baseUser.JobTitle;
                        cacheUser.CompanyId = baseUser.CompanyId;
                        cacheUser.SubCompanyId = baseUser.SubCompanyId;
                        cacheUser.DepartmentId = baseUser.DepartmentId;
                        cacheUser.WorkGroupId = baseUser.WorkGroupId;
                        cacheUser.RoleName = baseUser.RoleName;
                        cacheUser.JobTitleName = baseUser.JobTitleName;
                        cacheUser.CompanyName = baseUser.CompanyName;
                        cacheUser.SubCompanyName = baseUser.SubCompanyName;
                        cacheUser.DepartmentName = baseUser.DepartmentName;
                        cacheUser.WorkGroupName = baseUser.WorkGroupName;
                        RequestCache.AddCacheUser(cacheUser);

                        //获取权限信息放入缓存中
                        //操作按钮权限
                        if (CacheHelper.Get(ButtonPermissionKey) != null)
                        {
                            cacheSystem.ButtonPermissionObj = CacheHelper.Get(ButtonPermissionKey);
                        }
                        else
                        {
                            var tmp = baseStorageBll.GetButtonPermission(baseUser.Id);
                            CacheHelper.Insert(ButtonPermissionKey, tmp);
                            cacheSystem.ButtonPermissionObj = tmp;
                        }
                        //功能模块权限
                        if (CacheHelper.Get(ModulePermissionKey) != null)
                        {
                            cacheSystem.ModulePermissionObj = CacheHelper.Get(ModulePermissionKey);
                        }
                        else
                        {
                            var tmp = baseStorageBll.GetModulePermission(baseUser.Id);
                            CacheHelper.Insert(ModulePermissionKey, tmp);
                            cacheSystem.ModulePermissionObj = tmp;
                        }
                        //字段权限
                        if (CacheHelper.Get(FieldPermissionKey) != null)
                        {
                            cacheSystem.FieldPermissionObj = CacheHelper.Get(FieldPermissionKey);
                        }
                        else
                        {
                            var tmp = baseStorageBll.GetFieldPermission(baseUser.Id);
                            CacheHelper.Insert(FieldPermissionKey, tmp);
                            cacheSystem.FieldPermissionObj = tmp;
                        }
                        //数据权限
                        if (CacheHelper.Get(DataPermissionKey) != null)
                        {
                            cacheSystem.DataPermissionObj = CacheHelper.Get(DataPermissionKey);
                        }
                        else
                        {
                            var tmp = baseStorageBll.GetDataPermission(cacheUser);
                            CacheHelper.Insert(DataPermissionKey, tmp);
                            cacheSystem.DataPermissionObj = tmp;
                        }
                        //初始值侧边栏菜单
                        CacheTopModule cacheTopModule = new CacheTopModule();
                        List<BaseModulePermission> baseModulePermissionList = CacheHelper.Get(ModulePermissionKey) as List<BaseModulePermission>;
                        cacheTopModule.ModuleId = baseModulePermissionList.Where(u => u.ParentId == "0").Select(s => s.ModuleId).FirstOrDefault();
                        RequestCache.AddCacheTopModuleId(cacheTopModule);

                        baseAccess.Enabled = 1;
                        baseAccess.Description = string.Format("操作系统：{0}, 浏览器：{1}", PublicMethod.GetOSName(), PublicMethod.GetBrowse());
                        
                        //更新用户登陆次数
                        baseUser.LoginTimes = baseUser.LoginTimes + 1;
                        baseUser.LastLoginDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        baseUserDal.UpdateNoLog(baseUser);
                        msg.Msg = "登陆成功";
                        msg.Status = "+OK";
                        msg.BackUrl = "/InfoCenter/Dashboard/Index";
                        if (openAccessLog == "Open")
                        {
                            baseAccessBll.CreateNoLog(baseAccess);
                        }
                    }
                    else
                    {
                        baseAccess.Enabled = 0;
                        baseAccess.Description = string.Format("错误信息:{2},操作系统：{0}, 浏览器：{1}", PublicMethod.GetOSName(), PublicMethod.GetBrowse(), "密码错误");
                        msg.Msg = "密码错误";
                        msg.Status = "+ERROR";
                        msg.BackUrl = null;
                        if (openAccessLog == "Open")
                        {
                            baseAccessBll.CreateNoLog(baseAccess);
                        }
                    }
                }
                else
                {
                    baseAccess.Enabled = 0;
                    baseAccess.Description = string.Format("错误信息:{2},操作系统：{0}, 浏览器：{1}", PublicMethod.GetOSName(), PublicMethod.GetBrowse(), "账号被冻结");
                    msg.Msg = "账号被冻结";
                    msg.Status = "+ERROR";
                    msg.BackUrl = null;
                    if (openAccessLog == "Open")
                    {
                        baseAccessBll.CreateNoLog(baseAccess);
                    }
                }
            }
            else
            {
                baseAccess.Enabled = 0;
                baseAccess.Description = string.Format("错误信息:{2},操作系统：{0}, 浏览器：{1}", PublicMethod.GetOSName(), PublicMethod.GetBrowse(), "用户名不存在");
                msg.Msg = "用户名不存在";
                msg.Status = "+ERROR";
                msg.BackUrl = null;
                if (openAccessLog == "Open")
                {
                    baseAccessBll.CreateNoLog(baseAccess);
                }
            }
            return msg;
        }

        /// <summary>
        /// 获取用户组织机构树
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public string GetUserOrganizeListWhere(string where, string orderBy)
        {
            List<BaseTree> organizeTreeList = new List<BaseTree>();
            ConvertUserOrganize(baseUserDal.GetUserOrganizeListWhere(null, null), organizeTreeList, "0");
            return JsonConvert.SerializeObject(organizeTreeList);
        }

        /// <summary>
        /// 递归转换组织机构
        /// </summary>
        /// <param name="organizeList"></param>
        /// <param name="organizeTreeList"></param>
        /// <param name="parentId"></param>
        public static void ConvertUserOrganize(List<BaseOrganize> organizeList, List<BaseTree> organizeTreeList, string parentId)
        {
            foreach (var organize in organizeList.Where(o => o.ParentId == parentId))
            {
                BaseTree organizeTree = new BaseTree()
                {
                    id = organize.Id,
                    pid = organize.ParentId,
                    pname = organize.ParentName,
                    type = organize.Type,
                    target = "rightFrame",
                    url = "/SysManage/BaseUser/UserList?departmentId=" + organize.Id,
                    font = new font() { color = "#08c" },
                    code = organize.Code,
                    Amount = organize.Amount,
                    isleaf = organize.isleaf,
                    SortCode = organize.SortCode
                };
                if (organize.Enabled != 1)
                {
                    organizeTree.name = "<span style='text-decoration: line-through;'>" + organize.Name + "</span>";
                }
                else
                {
                    organizeTree.name = organize.Name;
                }
                organizeTree.children = new List<BaseTree>();
                organizeTreeList.Add(organizeTree);
                ConvertUserOrganize(organizeList, organizeTree.children, organizeTree.id);
            }
        }

        /// <summary>
        /// 得到部门所在的单位
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public BaseOrganize GetUserCompanyId(string departmentId)
        {
            return baseUserDal.GetUserCompanyId(departmentId);
        }

        /// <summary>
        /// 得到部门所在的分部
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public BaseOrganize GetUserSubCompanyId(string departmentId)
        {
            return baseUserDal.GetUserSubCompanyId(departmentId);
        }

          /// <summary>
        /// 获取用户数据权限类型标识
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetUserDataPermissionConstraint(string userId)
        {
            return baseUserDal.GetUserDataPermissionConstraint(userId);
        }
        /// <summary>
        /// 获取用户对应的功能模块权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetUserModuleListByUserId(string userId)
        {
            List<BaseTree> moduleTreeList = new List<BaseTree>();
            ConvertUserModule(baseUserDal.GetUserModuleListByUserId(userId), moduleTreeList, "0");
            return JsonConvert.SerializeObject(moduleTreeList);
        }

        /// <summary>
        /// 递归转换为用户模块树
        /// </summary>
        /// <param name="moduleList"></param>
        /// <param name="moduleTreeList"></param>
        /// <param name="parentId"></param>
        public void ConvertUserModule(List<BaseModule> moduleList, List<BaseTree> moduleTreeList, string parentId)
        {
            foreach (var module in moduleList.Where(o => o.ParentId == parentId))
            {
                BaseTree moduleTree = new BaseTree()
                {
                    id = module.Id,
                    name = module.Name,
                    pid = module.ParentId,
                    pname = module.ParentName,
                    code = module.Code,
                    font = new font() { color = "#08c" },
                    isChecked = module.isChecked
                };
                moduleTree.children = new List<BaseTree>();
                moduleTreeList.Add(moduleTree);
                ConvertUserModule(moduleList, moduleTree.children, moduleTree.id);
            }
        }

        /// <summary>
        /// 获取用户对应的操作按钮权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetUserButtonListByUserId(string userId)
        {
            List<BaseTree> buttonTreeList = new List<BaseTree>();
            ConvertUserButton(baseUserDal.GetUserButtonListByUserId(userId), buttonTreeList, "0");
            return JsonConvert.SerializeObject(buttonTreeList);
        }

        /// <summary>
        /// 递归转换为用户操作按钮树
        /// </summary>
        /// <param name="buttonList"></param>
        /// <param name="buttonTreeList"></param>
        /// <param name="parentId"></param>
        public void ConvertUserButton(List<BasePermissionItem> buttonList, List<BaseTree> buttonTreeList, string parentId)
        {
            foreach (var button in buttonList.Where(o => o.ParentId == parentId))
            {
                BaseTree buttonTree = new BaseTree()
                {
                    id = button.Id,
                    name = button.Name,
                    pid = button.ParentId,
                    pname = button.ParentName,
                    code = button.Code,
                    font = new font() { color = "#08c" },
                    isChecked = button.isChecked
                };
                buttonTree.children = new List<BaseTree>();
                buttonTreeList.Add(buttonTree);
                ConvertUserButton(buttonList, buttonTree.children, buttonTree.id);
            }
        }

        /// <summary>
        /// 获取用户对应的字段权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetUserFieldListByUserId(string userId)
        {
            List<BaseTree> buttonTreeList = new List<BaseTree>();
            ConvertUserField(baseUserDal.GetUserFieldListByUserId(userId), buttonTreeList, "0");
            return JsonConvert.SerializeObject(buttonTreeList);
        }

        /// <summary>
        /// 递归转换为用户操作按钮树
        /// </summary>
        /// <param name="buttonList"></param>
        /// <param name="buttonTreeList"></param>
        /// <param name="parentId"></param>
        public void ConvertUserField(List<BasePermissionItem> buttonList, List<BaseTree> buttonTreeList, string parentId)
        {
            foreach (var button in buttonList.Where(o => o.ParentId == parentId))
            {
                BaseTree buttonTree = new BaseTree()
                {
                    id = button.Id,
                    name = button.Name,
                    pid = button.ParentId,
                    pname = button.ParentName,
                    code = button.Code,
                    font = new font() { color = "#08c" },
                    isChecked = button.isChecked
                };
                buttonTree.children = new List<BaseTree>();
                buttonTreeList.Add(buttonTree);
                ConvertUserButton(buttonList, buttonTree.children, buttonTree.id);
            }
        }

          /// <summary>
        /// 获取用户对应的明细数据权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetUserDataDetailsListByUserId(string userId)
        {
            List<BaseTree> organizeTreeList = new List<BaseTree>();
            ConvertUserData(baseUserDal.GetUserDataDetailsListByUserId(userId), organizeTreeList, "0");
            return JsonConvert.SerializeObject(organizeTreeList);
        }

        /// <summary>
        /// 递归转换为用户明细数据权限树
        /// </summary>
        /// <param name="organizeList"></param>
        /// <param name="organizeTreeList"></param>
        /// <param name="parentId"></param>
        public void ConvertUserData(List<BaseOrganize> organizeList, List<BaseTree> organizeTreeList, string parentId)
        {
            foreach (var organize in organizeList.Where(o => o.ParentId == parentId))
            {
                BaseTree organizeTree = new BaseTree()
                {
                    id = organize.Id,
                    name = organize.Name,
                    pid = organize.ParentId,
                    pname = organize.ParentName,
                    code = organize.Code,
                    font = new font() { color = "#08c" },
                    isChecked = organize.isChecked
                };
                organizeTree.children = new List<BaseTree>();
                organizeTreeList.Add(organizeTree);
                ConvertUserData(organizeList, organizeTree.children, organizeTree.id);
            }
        }

        /// <summary>
        /// 保存用户对应对应的模块权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessValueArray"></param>
        /// <returns></returns>
        public bool UpdateUserModuleList(string id, string accessValueArray)
        {
            return baseUserDal.UpdateUserModuleList(id,accessValueArray);
        }

        /// <summary>
        /// 保存用户对应对应的操作按钮权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessValueArray"></param>
        /// <returns></returns>
        public bool UpdateUserButtonList(string id, string accessValueArray)
        {
            return baseUserDal.UpdateUserButtonList(id, accessValueArray);
        }

        /// <summary>
        /// 保存用户对应对应的字段权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessValueArray"></param>
        /// <returns></returns>
        public bool UpdateUserFieldList(string id, string accessValueArray)
        {
            return baseUserDal.UpdateUserFieldList(id, accessValueArray);
        }

          /// <summary>
        /// 保存用户对应对应的数据权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessValueArray"></param>
        /// <returns></returns>
        public bool UpdateUserDataList(string id, string accessValueArray, string constraint)
        {
            return baseUserDal.UpdateUserDataList(id, accessValueArray, constraint);
        }

         /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<BaseRole> GetUserRoleListWhere(string userId)
        {
            return baseUserDal.GetUserRoleListWhere(userId);
        }

         /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public bool SaveAllotRole(string userId, string roleIds)
        {
            return baseUserDal.SaveAllotRole(userId, roleIds);
        }
    }
}
