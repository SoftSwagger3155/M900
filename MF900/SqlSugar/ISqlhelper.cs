using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MF900
{
    public interface ISqlhelper<T> where T : class, new()
    {

        /// <summary>
        /// 增加单条数据
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <returns>操作是否成功</returns>
        Task<bool> Add(T model);

        /// <summary>
        /// 增加多条数据
        /// </summary>
        /// <param name="list">实体集合</param>
        /// <returns>操作是否成功</returns>
        Task<bool> AddRange(List<T> list);

        // <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetAll();

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <returns></returns>
        Task<T> GetByWhere(Expression<Func<T, bool>> where);
    }
}
