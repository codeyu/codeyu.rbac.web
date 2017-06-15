using Rbac.Entity;
using Rbac.IData;
using Rbac.Core;
using Newtonsoft.Json;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.Business
{
    public class BaseJobTitleBll : BaseBll<BaseJobTitle>
    {

        BaseOrganizeBll baseOrganizeBll = new BaseOrganizeBll();

        protected IBaseJobTitleDal baseJobTitleDal = null;
          public BaseJobTitleBll()
        {
            IApplicationContext springContext = ContextRegistry.GetContext();
            baseJobTitleDal = springContext.GetObject("BaseJobTitleDal") as IBaseJobTitleDal;
            base.idal = baseJobTitleDal;
        }


          // <summary>
          /// 获取组织机构树 
          /// </summary>
          /// <returns></returns>
          public string GetJobTitleOrganizeTree()
          {
              List<BaseTree> organizeTreeList = new List<BaseTree>();
              GetJobTitleOrganizeTree(baseOrganizeBll.GetListWhere(null, null), organizeTreeList, "0");
              return JsonConvert.SerializeObject(organizeTreeList);
          }


          /// <summary>
          /// 递归转换组织机构树
          /// </summary>
          /// <param name="organizeList"></param>
          /// <param name="organizeTreeList"></param>
          /// <param name="parentId"></param>
          public void GetJobTitleOrganizeTree(List<BaseOrganize> organizeList, List<BaseTree> organizeTreeList, string parentId)
          {
              foreach (var organize in organizeList.Where(o => o.ParentId == parentId))
              {
                  BaseTree baseTree = new BaseTree()
                  {
                      id = organize.Id,
                      pid = organize.ParentId,
                      pname = organize.ParentName,
                      url = "/SysManage/BaseJobTitle/JobTitleList?departmentId=" + organize.Id,
                      target = "rightFrame",
                      font = new font() { color = "#08c" },
                      code = organize.Code,
                      type = organize.Type,
                      Amount = organize.Amount,
                      isleaf = organize.isleaf,
                      SortCode = organize.SortCode
                  };
                  if (organize.Enabled != 1)
                  {
                      baseTree.name = "<span style='text-decoration: line-through;'>" + organize.Name + "</span>";
                  }
                  else
                  {
                      baseTree.name = organize.Name;
                  }
                  baseTree.children = new List<BaseTree>();
                  organizeTreeList.Add(baseTree);
                  GetJobTitleOrganizeTree(organizeList, baseTree.children, baseTree.id);
              }
          }
    }
}
