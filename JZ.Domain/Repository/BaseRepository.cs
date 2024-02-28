using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Domain.Repository
{
    public class BaseRepository<T> : SimpleClient<T> where T : class, new()
    {
        public BaseRepository(ISqlSugarClient db) 
        {
            base.Context = db;
        }

        /// <summary>
        /// 通用的添加方法
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<long> SaveModel(T t)
        {
            return await Context.Insertable(t).ExecuteReturnBigIdentityAsync();
        }
        /// <summary>
        /// 通用的逻辑删除方法
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<bool> DelLgicModel(T t)
        {
            return await Context.Updateable(t).SetColumns("Del", "1").ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<bool> DelModel(T t)
        {
            return await Context.Deleteable(t).ExecuteCommandHasChangeAsync();
        }
    }
}
