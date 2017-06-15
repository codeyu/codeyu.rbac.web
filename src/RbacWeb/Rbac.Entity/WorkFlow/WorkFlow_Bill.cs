//=====================================================================================
// All Rights Reserved , Copyright © Murphy 
//=====================================================================================

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
    /// <summary>
    ///  表单管理
    /// <author>
    ///		<name>lixinyu</name>
    ///		<date></date>
    /// </author>
    /// </summary>
    [Serializable]
    [TableName("WorkFlow_Bill")]
    [PrimaryKey("Id")]
    [Description("表单管理")]
    [Key("Id")]
    public class WorkFlow_Bill
    {
        private string _Id = null;
        /// <summary>
        /// 表单主键
        /// </summary>
        /// <returns></returns>
        [Description("表单主键")]
        public string Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this._Id = value;
            }
        }
        private string _Name = null;
        /// <summary>
        /// 表单名称
        /// </summary>
        /// <returns></returns>
        [Description("表单名称")]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }
        private string _Description = null;
        /// <summary>
        /// 备注说明
        /// </summary>
        /// <returns></returns>
        [Description("备注说明")]
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;
            }
        }
        private string _TableName = null;
        /// <summary>
        /// 表单主表
        /// </summary>
        /// <returns></returns>
        [Description("表单主表")]
        public string TableName
        {
            get
            {
                return this._TableName;
            }
            set
            {
                this._TableName = value;
            }
        }
        private string _DetailKeyField = null;
        /// <summary>
        /// 表单主子表关联键
        /// </summary>
        /// <returns></returns>
        [Description("表单主子表关联键")]
        public string DetailKeyField
        {
            get
            {
                return this._DetailKeyField;
            }
            set
            {
                this._DetailKeyField = value;
            }
        }
        private int? _Enabled = null;
        /// <summary>
        /// 是否启用
        /// </summary>
        /// <returns></returns>
        [Description("是否启用")]
        public int? Enabled
        {
            get
            {
                return this._Enabled;
            }
            set
            {
                this._Enabled = value;
            }
        }
        private int? _SortCode = null;
        /// <summary>
        /// 排序值
        /// </summary>
        /// <returns></returns>
        [Description("排序值")]
        public int? SortCode
        {
            get
            {
                return this._SortCode;
            }
            set
            {
                this._SortCode = value;
            }
        }
        private string _CreateUserId = null;
        /// <summary>
        /// 创建人主键
        /// </summary>
        /// <returns></returns>
        [Description("创建人主键")]
        public string CreateUserId
        {
            get
            {
                return this._CreateUserId;
            }
            set
            {
                this._CreateUserId = value;
            }
        }
        private DateTime? _CreateDate = null;
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        [Description("创建日期")]
        public DateTime? CreateDate
        {
            get
            {
                return this._CreateDate;
            }
            set
            {
                this._CreateDate = value;
            }
        }
        private string _ModifyUserId = null;
        /// <summary>
        /// 修改人主键
        /// </summary>
        /// <returns></returns>
        [Description("修改人主键")]
        public string ModifyUserId
        {
            get
            {
                return this._ModifyUserId;
            }
            set
            {
                this._ModifyUserId = value;
            }
        }
        private DateTime? _ModifyDate = null;
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        [Description("修改日期")]
        public DateTime? ModifyDate
        {
            get
            {
                return this._ModifyDate;
            }
            set
            {
                this._ModifyDate = value;
            }
        }
    }
}