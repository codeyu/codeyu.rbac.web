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
    ///  表单明细表
    /// <author>
    ///		<name>lixinyu</name>
    ///		<date></date>
    /// </author>
    /// </summary>
    [Serializable]
    [TableName("WorkFlow_BillDetailTable")]
    [PrimaryKey("Id")]
    [Description("表单明细表")]
    [Key("Id")]
    public class WorkFlow_BillDetailTable
    {
        private string _Id = null;
        /// <summary>
        /// 明细表主键
        /// </summary>
        /// <returns></returns>
        [Description("明细表主键")]
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
        private string _TableName = null;
        /// <summary>
        /// 明细表名
        /// </summary>
        /// <returns></returns>
        [Description("明细表名")]
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
    }
}