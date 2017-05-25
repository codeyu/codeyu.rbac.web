using Murphy.Entity;
using Murphy.IData;
using Murphy.Core;
using Newtonsoft.Json;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murphy.Business
{
    public class BaseRoleBll : BaseBll<BaseRole>
    {
        protected IBaseRoleDal baseRoleDal = null;
        public BaseRoleBll()
        {
            IApplicationContext springContext = ContextRegistry.GetContext();
            baseRoleDal = springContext.GetObject("BaseRoleDal") as IBaseRoleDal;
            base.idal = baseRoleDal;
        }

        /// <summary>
        /// 删除角色（带权限信息）
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public bool DeleteRole(string roleId)
        {
            return baseRoleDal.DeleteRole(roleId);
        }

        /// <summary>
        /// 获取指定部门下的角色
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public List<BaseRole> GetRoleListByDepartmentId(string departmentId)
        {
            return baseRoleDal.GetRoleListByDepartmentId(departmentId);
        }

        

        /// <summary>
        /// 获取用户数据权限类型标识
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public string GetRoleDataPermissionConstraint(string roleId)
        {
            return baseRoleDal.GetRoleDataPermissionConstraint(roleId);
        }

        /// <summary>
        /// 获取角色对应的功能模块权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public string GetRoleModuleListByRoleId(string roleId)
        {
            List<BaseTree> moduleTreeList = new List<BaseTree>();
            ConvertRoleModule(baseRoleDal.GetRoleModuleListByRoleId(roleId), moduleTreeList, "0");
            return JsonConvert.SerializeObject(moduleTreeList);
        }

        /// <summary>
        /// 递归转换为角色模块树
        /// </summary>
        /// <param name="moduleList"></param>
        /// <param name="moduleTreeList"></param>
        /// <param name="parentId"></param>
        public void ConvertRoleModule(List<BaseModule> moduleList, List<BaseTree> moduleTreeList, string parentId)
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
                ConvertRoleModule(moduleList, moduleTree.children, moduleTree.id);
            }
        }


        /// <summary>
        /// 获取角色对应的操作按钮权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public string GetRoleButtonListByRoleId(string roleId)
        {
            List<BaseTree> buttonTreeList = new List<BaseTree>();
            ConvertRoleButton(baseRoleDal.GetRoleButtonListByRoleId(roleId), buttonTreeList, "0");
            return JsonConvert.SerializeObject(buttonTreeList);
        }

        /// <summary>
        /// 递归转换为角色操作按钮树
        /// </summary>
        /// <param name="buttonList"></param>
        /// <param name="buttonTreeList"></param>
        /// <param name="parentId"></param>
        public void ConvertRoleButton(List<BasePermissionItem> buttonList, List<BaseTree> buttonTreeList, string parentId)
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
                ConvertRoleButton(buttonList, buttonTree.children, buttonTree.id);
            }
        }


        /// <summary>
        /// 获取角色对应的明细数据权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public string GetRoleDataDetailsListByRoleId(string roleId)
        {
            List<BaseTree> organizeTreeList = new List<BaseTree>();
            ConvertRoleData(baseRoleDal.GetRoleDataDetailsListByRoleId(roleId), organizeTreeList, "0");
            return JsonConvert.SerializeObject(organizeTreeList);
        }

        /// <summary>
        /// 递归转换为角色明细数据权限树
        /// </summary>
        /// <param name="organizeList"></param>
        /// <param name="organizeTreeList"></param>
        /// <param name="parentId"></param>
        public void ConvertRoleData(List<BaseOrganize> organizeList, List<BaseTree> organizeTreeList, string parentId)
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
                ConvertRoleData(organizeList, organizeTree.children, organizeTree.id);
            }
        }

        /// <summary>
        /// 获取角色对应的字段权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public string GetRoleFieldListByRoleId(string roleId)
        {

            List<BaseTree> buttonTreeList = new List<BaseTree>();
            ConvertRoleField(baseRoleDal.GetRoleFieldListByRoleId(roleId), buttonTreeList, "0");
            return JsonConvert.SerializeObject(buttonTreeList);
        }

        /// <summary>
        /// 递归转换为角色操作按钮树
        /// </summary>
        /// <param name="buttonList"></param>
        /// <param name="buttonTreeList"></param>
        /// <param name="parentId"></param>
        public void ConvertRoleField(List<BasePermissionItem> buttonList, List<BaseTree> buttonTreeList, string parentId)
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
                ConvertRoleField(buttonList, buttonTree.children, buttonTree.id);
            }
        }

        /// <summary>
        /// 保存角色对应的模块权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessValueArray"></param>
        /// <returns></returns>
        public bool UpdateRoleModuleList(string id, string accessValueArray)
        {
            return baseRoleDal.UpdateRoleModuleList(id, accessValueArray);
        }

        /// <summary>
        /// 保存角色对应的操作按钮权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessValueArray"></param>
        /// <returns></returns>
        public bool UpdateRoleButtonList(string id, string accessValueArray)
        {
            return baseRoleDal.UpdateRoleButtonList(id, accessValueArray);
        }

        /// <summary>
        /// 保存角色对应的字段权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessValueArray"></param>
        /// <returns></returns>
        public bool UpdateRoleFieldList(string id, string accessValueArray)
        {
            return baseRoleDal.UpdateRoleFieldList(id, accessValueArray);
        }

        /// <summary>
        /// 保存角色对应的数据权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessValueArray"></param>
        /// <returns></returns>
        public bool UpdateRoleDataList(string id, string accessValueArray, string constraint)
        {
            return baseRoleDal.UpdateRoleDataList(id, accessValueArray, constraint);
        }
    }
}
