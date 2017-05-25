//=====================================================================================
// All Rights Reserved , Copyright © Murphy 
//=====================================================================================

using Murphy.Core;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murphy.Entity
{
    /// <summary>
    /// 表单字段
    /// <author>
    ///		<name>lixinyu</name>
    ///		<date></date>
    /// </author>
    /// </summary>
    [Serializable]
    [TableName("WorkFlow_BillField")]
    [PrimaryKey("Id")]
    [Description("表单字段")]
    [Key("Id")]
    public class WorkFlow_BillField
    {
        private string _Id = null;
        /// <summary>
        /// 字段主键
        /// </summary>
        /// <returns></returns>
        [Description("字段主键")]
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
        private string _BillId = null;
        /// <summary>
        /// 表单主键
        /// </summary>
        /// <returns></returns>
        [Description("表单主键")]
        public string BillId
        {
            get
            {
                return this._BillId;
            }
            set
            {
                this._BillId = value;
            }
        }
        private string _FieldName = null;
        /// <summary>
        /// 字段名称
        /// </summary>
        /// <returns></returns>
        [Description("字段名称")]
        public string FieldName
        {
            get
            {
                return this._FieldName;
            }
            set
            {
                this._FieldName = value;
            }
        }
        private string _DisplayName = null;
        /// <summary>
        /// 显示名称
        /// </summary>
        /// <returns></returns>
        [Description("显示名称")]
        public string DisplayName
        {
            get
            {
                return this._DisplayName;
            }
            set
            {
                this._DisplayName = value;
            }
        }
        private string _FieldType = null;
        /// <summary>
        /// 字段类型
        /// </summary>
        /// <returns></returns>
        [Description("字段类型")]
        public string FieldType
        {
            get
            {
                return this._FieldType;
            }
            set
            {
                this._FieldType = value;
            }
        }
        private int? _HtmlShowType = null;
        /// <summary>
        /// HTML展现类型
        /// </summary>
        /// <returns></returns>
        [Description("HTML展现类型")]
        public int? HtmlShowType
        {
            get
            {
                return this._HtmlShowType;
            }
            set
            {
                this._HtmlShowType = value;
            }
        }
        private int? _HtmlShowDetailType = null;
        /// <summary>
        /// HTML展现形式
        /// </summary>
        /// <returns></returns>
        [Description("HTML展现形式")]
        public int? HtmlShowDetailType
        {
            get
            {
                return this._HtmlShowDetailType;
            }
            set
            {
                this._HtmlShowDetailType = value;
            }
        }
        private int? _ViewType = null;
        /// <summary>
        /// 状态
        /// </summary>
        /// <returns></returns>
        [Description("状态")]
        public int? ViewType
        {
            get
            {
                return this._ViewType;
            }
            set
            {
                this._ViewType = value;
            }
        }
        private int? _TextHeight = null;
        /// <summary>
        /// 文本框高度
        /// </summary>
        /// <returns></returns>
        [Description("文本框高度")]
        public int? TextHeight
        {
            get
            {
                return this._TextHeight;
            }
            set
            {
                this._TextHeight = value;
            }
        }
        private string _DetailTableName = null;
        /// <summary>
        /// 明细表名称
        /// </summary>
        /// <returns></returns>
        [Description("明细表名称")]
        public string DetailTableName
        {
            get
            {
                return this._DetailTableName;
            }
            set
            {
                this._DetailTableName = value;
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