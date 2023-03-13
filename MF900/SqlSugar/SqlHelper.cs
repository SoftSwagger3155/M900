using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace MF900
{
    public class SqlSugarHelper<T> : ISqlhelper<T> where T : class, new()
    {
        //构造函数
        public SqlSugarHelper() { }
        ~SqlSugarHelper() { }

        private ConnectionConfig connectionConfig;

        public ConnectionConfig SetConectionConfigs
        {
            get { return connectionConfig; }
            set { connectionConfig = value; }
        }

        public ConnectionConfig SetConnectionConfig(string sqlPath,DbType dbType)
        {
            return new ConnectionConfig()
            {
                ConnectionString = $"DataSource={ Environment.CurrentDirectory}\\{ sqlPath }.DB;Pooling=true;FailIfMissing=false",// + sqlName +
                DbType = dbType,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute,
            };
        }

        /// <summary>
        /// CodeFirst 新建表格
        /// </summary>
        /// <returns></returns>
        public void CodeFirst()
        {
            using (SqlSugarClient db = new SqlSugarClient(connectionConfig))
            {
                db.CodeFirst.InitTables<T>();
            }
        }
        
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Add(T model)
        {
            using (SqlSugarClient db = new SqlSugarClient(connectionConfig))
            {
                return await db.Insertable(model).ExecuteCommandAsync() > 0;
            }
        }
        /// <summary>
        /// 添加多条数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<bool> AddRange(List<T> list)
        {
            using (SqlSugarClient db = new SqlSugarClient(connectionConfig))
            {
                return await db.Insertable(list).ExecuteCommandAsync() > 0;
            }
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> UpdateDate(Expression<Func<T, T>> columns, Expression<Func<T, bool>> where)
        {
            using (SqlSugarClient db = new SqlSugarClient(connectionConfig))
            {
                return await db.Updateable<T>(columns).Where(where).ExecuteCommandAsync() > 0;
            }
        }
        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> GetAll()
        {
            using (SqlSugarClient db = new SqlSugarClient(connectionConfig))
            {
                return await db.Queryable<T>().ToListAsync() ;
            }
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <returns></returns>
        public async Task<T> GetByWhere(Expression<Func<T, bool>> where)
        {
            using (SqlSugarClient db = new SqlSugarClient(connectionConfig))
            {
                return await db.Queryable<T>().FirstAsync(where);
            }
        }
        /// <summary>
        /// 条件查询(返回DataTable)
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<System.Data.DataTable> GetToDataTable(Expression<Func<T, bool>> where)
        {
            using (SqlSugarClient db = new SqlSugarClient(connectionConfig))
            {
                return await db.Queryable<T>().Where(where).ToDataTableAsync();
            }
        }
        /// <summary>
        /// 条件查询数量
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<int> GetByCount(Expression<Func<T, bool>> where)
        {
            using (SqlSugarClient db = new SqlSugarClient(connectionConfig))
            {
                return await db.Queryable<T>().CountAsync(where);
            }
        }
        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="pkValue"></param>
        /// <returns></returns>
        public async Task<T> GetInSingle(object pkValue)
        {
            using (SqlSugarClient db = new SqlSugarClient(connectionConfig))
            {
                return await db.Queryable<T>().InSingleAsync(pkValue);
            }
        }

        /// <summary>
        /// 删除单个对象
        /// </summary>
        /// <param name="pkValue"></param>
        /// <returns></returns>
        public async Task<bool> DelelteInSingle(T pkValue)
        {
            using (SqlSugarClient db = new SqlSugarClient(connectionConfig))
            {
                return await db.Deleteable<T>(pkValue).ExecuteCommandAsync() > 0;
            }
        }

        /// <summary>
        /// 条件删除
        /// </summary>
        /// <param name="primaryKeyValue"></param>
        /// <returns></returns>
        public Task<int> DelelteByWhere(Expression<Func<T, bool>> where)
        {
            using (SqlSugarClient db = new SqlSugarClient(connectionConfig))
            {
                return db.Deleteable<T>(where).ExecuteCommandAsync();
            }
        }
    }
}
