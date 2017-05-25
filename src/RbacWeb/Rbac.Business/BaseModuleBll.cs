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
    public class BaseModuleBll : BaseBll<BaseModule>
    {
        protected IBaseModuleDal baseModuleDal = null;
        public BaseModuleBll()
        {
            IApplicationContext springContext = ContextRegistry.GetContext();
            baseModuleDal = springContext.GetObject("BaseModuleDal") as IBaseModuleDal;
            base.idal = baseModuleDal;
        }

        /// <summary>
        /// 获取功能模块树  通用功能模块时需要
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public string GetModuleListWhere(string where, string orderBy, string moduleId)
        {
            List<BaseTree> moduleTreeList = new List<BaseTree>();
            ConvertModule(baseModuleDal.GetListWhere(null, null), moduleTreeList, "0", moduleId);
            return JsonConvert.SerializeObject(moduleTreeList);
        }

     /// <summary>
        /// 递归转换功能模块
     /// </summary>
     /// <param name="moduleList"></param>
     /// <param name="moduleTreeList"></param>
     /// <param name="parentId"></param>
     /// <param name="moduleId"></param>
        public void ConvertModule(List<BaseModule> moduleList, List<BaseTree> moduleTreeList, string parentId, string moduleId)
        {
            foreach (var module in moduleList.Where(o => o.ParentId == parentId))
            {

                BaseTree moduleTree = new BaseTree()
                {
                    id = module.Id,
                    pid = module.ParentId,
                    name = module.Name,
                    pname = module.ParentName,
                    font = new font() { color = "#08c" },
                    code = module.Code,
                    Amount = module.Amount,
                    isleaf = module.isleaf,
                    ModuleSortCode = module.SortCode
                };
                if (module.Id == moduleId)
                {
                    moduleTree.isChecked = true;
                }
                moduleTree.children = new List<BaseTree>();
                moduleTreeList.Add(moduleTree);
                ConvertModule(moduleList, moduleTree.children, moduleTree.id, moduleId);
            }
        }

    }
}
