using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.Entity
{
    public class BaseTree
    {
        /// <summary>
        /// 节点主键
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 父级节点主键
        /// </summary>
        public string pid { get; set; }
        /// <summary>
        /// 节点编码
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 父级节点名称
        /// </summary>
        public string pname { get; set; }
        /// <summary>
        /// 上级节点编码
        /// </summary>
        public string pcode { get; set; }
        /// <summary>
        /// 展开节点
        /// </summary>
        public bool open { get; set; }
        /// <summary>
        /// 目标地址
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 节点类型
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 是否能够选中
        /// </summary>
        public bool nocheck { get; set; }
        /// <summary>
        /// 目标窗口
        /// </summary>
        public string target { get; set; }
        /// <summary>
        /// 字体信息
        /// </summary>
        public font font { get; set; }
        /// <summary>
        /// 排序值
        /// </summary>
        public int SortCode { get; set; }
        /// <summary>
        /// 功能模块排序值  功能模块排序值是varchar类型
        /// </summary>
        public string ModuleSortCode { get; set; }
        /// <summary>
        /// 操作权限项排序值  操作权限项排序值是varchar类型
        /// </summary>
        public string PermissionItemSortCode { get; set; }
        /// <summary>
        /// 节点选中
        /// </summary>
        public bool isChecked { get; set; }
        /// <summary>
        /// 子节点个数
        /// </summary>
        public string Amount { get; set; }
        /// <summary>
        /// 是否是叶子节点
        /// </summary>
        public bool isleaf { get; set; }
        /// <summary>
        /// 子节点
        /// </summary>
        public List<BaseTree> children { get; set; }
    }
}
