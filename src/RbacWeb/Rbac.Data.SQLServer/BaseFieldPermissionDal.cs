using Murphy.Entity;
using Murphy.IData;
using Murphy.Utils;
using Murphy.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murphy.Data.SQLServer
{
    public class BaseFieldPermissionDal : IBaseFieldPermissionDal
    {
        BaseStorageDal baseStorageDal = new BaseStorageDal();
        //单利模式
        private static BaseFieldPermissionDal baseFieldPermissionDal;
        public static BaseFieldPermissionDal Instance
        {
            get
            {
                if (baseFieldPermissionDal == null)
                {
                    baseFieldPermissionDal = new BaseFieldPermissionDal();
                }
                return baseFieldPermissionDal;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseFieldPermissionDal()
        {
            //获取用户对应的字段权限
            var baseFieldPermissionList = RequestCache.GetCacheSystem().FieldPermissionObj as List<BaseFieldPermission>;
            //可展示的列名集合
            List<string> filterUserFieldList = new List<string>();
            List<string> filterOrganizeFieldList = new List<string>();
            //过滤步骤
            if (baseFieldPermissionList != null)
            {
                //过滤用户信息
                foreach (var baseFieldPermission in baseFieldPermissionList.Where(f => f.ParentCode == "AccessUserItem").ToList())
                {
                    filterUserFieldList.Add(baseFieldPermission.FieldCode);
                }

                //过滤组织机构信息
                foreach (var baseFieldPermission in baseFieldPermissionList.Where(f => f.ParentCode == "AccessOrganizeItem").ToList())
                {
                    filterOrganizeFieldList.Add(baseFieldPermission.FieldCode);
                }

                this.User = filterUserFieldList;
                this.Organization = filterOrganizeFieldList;
            }
        }

        /// <summary>
        /// 获取过滤后的用户信息
        /// </summary>
        /// <param name="userList"></param>
        /// <returns></returns>
        public List<BaseUser> FilterUser(List<BaseUser> userList)
        {
            DataTable dt = DataTableHelper.ToDataTable(userList);
            foreach (var column in this.User.ToArray())
            {
                dt.Columns.Remove(column);
            }
            //dt = dt.DefaultView.ToTable(false, this.User.ToArray());
            //dt = dt.DefaultView.ToTable(false, new string[] { "RealName" });
            return DataTableHelper.ToList<BaseUser>(dt);
        }

        /// <summary>
        /// 获取过滤后的组织机构信息
        /// </summary>
        /// <param name="organizeList"></param>
        /// <returns></returns>
        public List<BaseOrganize> FilterOrganize(List<BaseOrganize> organizeList)
        {
            DataTable dt = DataTableHelper.ToDataTable(organizeList);
            dt = dt.DefaultView.ToTable(false, this.Organization.ToArray());
            return DataTableHelper.ToList<BaseOrganize>(dt);
        }

        /// <summary>
        /// 用户
        /// </summary>
        public List<string> User { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public List<string> Organization { get; set; }
    }
}
