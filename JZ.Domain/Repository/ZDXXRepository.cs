using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Domain.Repository
{
    /// <summary>
    /// 字典信息管理
    /// </summary>
    public class ZDXXRepository : BaseRepository<Domain.Entitys.JZ_SYS_ZDXX>
    {
        private readonly ISqlSugarClient db;

        public ZDXXRepository(ISqlSugarClient db) : base(db)
        {
            this.db = db;
        }
    }
}
