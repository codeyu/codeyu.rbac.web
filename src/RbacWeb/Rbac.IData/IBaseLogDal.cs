using Rbac.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbac.IData
{
    public interface IBaseLogDal : IBaseDal<BaseLog>
    {
        /// <summary>
        /// 添加创建日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
       void AddCreateLog<T>(T entity);

        /// <summary>
        /// 添加编辑日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oldEntity"></param>
        /// <param name="newEntity"></param>
        void AddUpdateLog<T>(T oldEntity, T newEntity);

        /// <summary>
        /// 添加删除日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        void AddDeleteLog<T>(T entity);


        /// <summary>
        /// 添加其他操作日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        void AddOtherLog<T>(T entity);
    }
}
